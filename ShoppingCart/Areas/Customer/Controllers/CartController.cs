using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using ShoppingCart.Utility;
using ShoppingCart.ViewModels;
using Stripe.Checkout;
using System.Security.Claims;


namespace ShoppingCart.Areas.Customer.Controllers
{
    
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CartVM vm { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity=(ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            vm = new CartVM()
            {
                Cart = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product"),
                OrderHeader = new OrderHeader()
            };


            foreach(var item in vm.Cart)
            {
                vm.OrderHeader.OrderTotal += (item.Product.Price*item.Count);
            }

            return View(vm);
        }

		[HttpGet]
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            vm = new CartVM()
            {
                Cart= _unitOfWork.Cart.GetAll(x=>x.ApplicationUserId == claims.Value,
                includeProperties: "Product"),
                OrderHeader = new OrderHeader()
            };

            vm.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetT(x => x.Id == claims.Value);
            vm.OrderHeader.Name = vm.OrderHeader.ApplicationUser.Name;
            vm.OrderHeader.Phone = vm.OrderHeader.ApplicationUser.PhoneNumber;
            vm.OrderHeader.Address = vm.OrderHeader.ApplicationUser.Address;
            vm.OrderHeader.City = vm.OrderHeader.ApplicationUser.City;
            vm.OrderHeader.State = vm.OrderHeader.ApplicationUser.State;
            vm.OrderHeader.PostalCode = vm.OrderHeader.ApplicationUser.PinCode;

            foreach(var item in vm.Cart)
            {
                vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
            }

            return View(vm);
        }

        [HttpPost]
		
		public IActionResult Summary(CartVM vm)
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			vm.Cart = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product");
			vm.OrderHeader.OrderStatus = OrderStatus.StatusPending;
			vm.OrderHeader.PaymentStatus = PaymentStatus.StatusPending;
			vm.OrderHeader.DateOfOrder = DateTime.Now;
			vm.OrderHeader.ApplicationUserId = claims.Value;
			foreach (var item in vm.Cart)
			{
				vm.OrderHeader.OrderTotal += (item.Product.Price * item.Count);
			}

			_unitOfWork.OrderHeader.Add(vm.OrderHeader);
			_unitOfWork.Save();

			foreach (var item in vm.Cart)
			{
				OrderDetail orderDetail = new OrderDetail()
				{
					ProductId = item.ProductId,
					OrderHeaderId = vm.OrderHeader.id,
					Count = item.Count,
					Price = item.Product.Price
				};
				_unitOfWork.OrderDetail.Add(orderDetail);
				_unitOfWork.Save();
			}
			var domain = "https://localhost:7248/";
			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>(),
				Mode = "payment",
				SuccessUrl = domain + $"Customer/Cart/ordersuccess?id={vm.OrderHeader.id}",
				CancelUrl = domain + $"customer/cart/Index",
			};

			foreach (var item in vm.Cart)
			{
				var lineItemOptions = new SessionLineItemOptions
				{
					PriceData = new SessionLineItemPriceDataOptions
					{
						UnitAmount = (long)(item.Product.Price * 100),//20.00 -> 2000
						Currency = "INR",
						ProductData = new SessionLineItemPriceDataProductDataOptions
						{
							Name = item.Product.Name,
						},

					},
					Quantity = item.Count,
				};
				options.LineItems.Add(lineItemOptions);
			}

			var service = new SessionService();
			Session session = service.Create(options);

			_unitOfWork.OrderHeader.PaymentStatus(vm.OrderHeader.id, session.Id, session.PaymentIntentId);
			_unitOfWork.Save();

			_unitOfWork.Cart.DeleteRange(vm.Cart);
			_unitOfWork.Save();

			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);



			return RedirectToAction("Index", "Home");



        }



        
		public IActionResult ordersuccess(int id)
        {
            var orderHeader = _unitOfWork.OrderHeader.GetT(x => x.id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);

            if (session.PaymentStatus.ToLower() == "paid")
            {
				_unitOfWork.OrderHeader.UpdateStatus(id, OrderStatus.StatusApproved, PaymentStatus.PaymentStatusApproved);
			}
			List<Cart> cart = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            _unitOfWork.Cart.DeleteRange(cart);
            _unitOfWork.Save();

            return View(id);
        }


		private double GetPriceBasedOnQuantity(double quantity, double price)
		{
			if (quantity <= 50)
			{
				return price;
			}
			else
			{
				if (quantity <= 100)
				{
					return price;
				}
				return price;
			}
		}



		public IActionResult plus(int id)
        {
            var cart = _unitOfWork.Cart.GetT(x=>x.Id==id);
            _unitOfWork.Cart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult minus(int id)
        {
            var cart = _unitOfWork.Cart.GetT(x=>x.Id==id);
            if(cart.Count<=1)
            {
                _unitOfWork.Cart.Delete(cart);

                var count = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == cart.ApplicationUserId)
                .ToList().Count - 1;
                HttpContext.Session.SetInt32("SessionCart", count);
            }
            else
            {
                _unitOfWork.Cart.DecrementCount(cart,1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }  

        public IActionResult delete(int id)
        {
            var cart = _unitOfWork.Cart.GetT(x=>x.Id==id);
            _unitOfWork.Cart.Delete(cart);
            _unitOfWork.Save();

            var count = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == cart.ApplicationUserId)
                .ToList().Count;
            HttpContext.Session.SetInt32("SessionCart", count);

            return RedirectToAction(nameof(Index));
        }

    }
}
