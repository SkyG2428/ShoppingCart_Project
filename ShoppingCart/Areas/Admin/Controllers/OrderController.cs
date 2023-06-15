using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using ShoppingCart.Utility;
using ShoppingCart.ViewModels;
using Stripe;
using Stripe.Checkout;
using System.Drawing;
using System.Security.Claims;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public OrderController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult AllOrders(string status)
        {


            //IEnumerable<OrderHeader> orderHeader;
            //if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            //{
            //    orderHeader = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            //}
            //else
            //{
            //    var claimsIdentity=(ClaimsIdentity)User.Identity;
            //    var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //    orderHeader = _unitOfWork.OrderHeader.GetAll(x => x.ApplicationUserId == claims.Value);
            //}


            IEnumerable<OrderHeader> orderHeaders;

            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                orderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                orderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == claim.Value);
            }

            switch (status)
            {
                case "pending":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == PaymentStatus.StatusPaymentDelayed);
                    break;
                case "approved":
                    orderHeaders = orderHeaders.Where(u => u.PaymentStatus == PaymentStatus.PaymentStatusApproved);
                    break;
                case "underprocess":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == OrderStatus.StatusInProcess);
                    break;
                case "shipped":
                    orderHeaders = orderHeaders.Where(u => u.OrderStatus == OrderStatus.StatusShipped);
                    break;
                default:
                    break;
            }


            return Json(new { data = orderHeaders });
        }


        public IActionResult Index()
        {
            return View();
        }
    
    

        public IActionResult OrderDetails(int id)
        {
            OrderVM orderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetT(x => x.id == id, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(x => x.OrderHeaderId == id, includeProperties: "Product")
            };
            return View(orderVM);
            
        }

        [Authorize(Roles =WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        [HttpPost]
        public IActionResult OrderDetails(OrderVM vm)
        {
            var orderHeader = _unitOfWork.OrderHeader.GetT(x => x.id == vm.OrderHeader.id);
            orderHeader.Name = vm.OrderHeader.Name;
            orderHeader.Phone = vm.OrderHeader.Phone;
            orderHeader.Address = vm.OrderHeader.Address;
            orderHeader.City = vm.OrderHeader.City;
            orderHeader.State = vm.OrderHeader.State;
            orderHeader.PostalCode = vm.OrderHeader.PostalCode;
            if(vm.OrderHeader.Carrier !=null)
            {
                orderHeader.Carrier= vm.OrderHeader.Carrier;
            }
            if(vm.OrderHeader.TrackingNumber!=null)
            {
                orderHeader.TrackingNumber= vm.OrderHeader.TrackingNumber;
            }
            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            TempData["success"] = "Info Updated";
            return RedirectToAction("OrderDetails", "Order", new {id = vm.OrderHeader.id });

        }


        [Authorize(Roles =WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        public IActionResult InProcess(OrderVM vm)
        {
            _unitOfWork.OrderHeader.UpdateStatus(vm.OrderHeader.id, OrderStatus.StatusInProcess);
            _unitOfWork.Save();
            TempData["success"] = "Order Status Updated-Inprocess";
            return RedirectToAction("OrderDetails", "Order", new { id = vm.OrderHeader.id });
        }

        [Authorize(Roles = WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        public IActionResult Shipped(OrderVM vm)
        {
            var orderHeader = _unitOfWork.OrderHeader.GetT(x => x.id == vm.OrderHeader.id);
            orderHeader.Carrier = vm.OrderHeader.Carrier;
            orderHeader.TrackingNumber= vm.OrderHeader.TrackingNumber;
            orderHeader.OrderStatus = OrderStatus.StatusShipped;
            orderHeader.DateOfShipping = DateTime.Now;

            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            TempData["success"] = "Order Status Updated-Shipped";
            return RedirectToAction("OrderDetails", "Order", new { id = vm.OrderHeader.id });
        }


        [Authorize(Roles = WebSiteRole.Role_Admin + "," + WebSiteRole.Role_Employee)]
        public IActionResult CancelOrder(OrderVM vm)
        {
            var orderHeader = _unitOfWork.OrderHeader.GetT(u => u.id == vm.OrderHeader.id);
            if (orderHeader.PaymentStatus == PaymentStatus.PaymentStatusApproved)
            {
               

				var refund = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentIntentId
                };
                var service = new RefundService();
                Refund Refund = service.Create(refund);
                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.id, OrderStatus.StatusCancelled, OrderStatus.StatusRefunded);


            }
            else
            {
                _unitOfWork.OrderHeader.UpdateStatus(orderHeader.id, OrderStatus.StatusCancelled, OrderStatus.StatusCancelled);

            }

            _unitOfWork.Save();
            TempData["success"] = "Order Cancelled";
            return RedirectToAction("OrderDetails", "Order", new { id = vm.OrderHeader.id });
        }


        public IActionResult PayNow(OrderVM vm)
        {
            var OrderHeader = _unitOfWork.OrderHeader.GetT(x => x.id == vm.OrderHeader.id, includeProperties: "ApplicationUser");
            var OrderDetail = _unitOfWork.OrderDetail.GetAll(x => x.OrderHeaderId == vm.OrderHeader.id, includeProperties: "Product");
            var domain = "https://localhost:7248/";
            var option = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"customer/cart/ordersuccess?id={vm.OrderHeader.id}",
                CancelUrl = domain + $"customer/cart/Index",
            };

            foreach (var item in OrderDetail)
            {
                var lineItemsOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "INR",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },
                    },
                    Quantity = item.Count,
                };
                option.LineItems.Add(lineItemsOptions);
            }
            var service=new SessionService();
            Session session = service.Create(option);
            _unitOfWork.OrderHeader.PaymentStatus(vm.OrderHeader.id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            return RedirectToAction("Index", "Home");
        }
    }

}
