using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeStoreDataBaseLibrary;
using BikeStoreDataBaseLibrary.Entity;

namespace BikeStoreCrudeEFDBFirstApproch.Areas.OrderItems.Controllers
{
    [Area("OrderItems")]
    public class OrderItemsController : Controller
    {
        private readonly BikestoreContext _context;

        public OrderItemsController(BikestoreContext context)
        {
            _context = context;
        }

        // GET: OrderItems/OrderItems
        public async Task<IActionResult> Index()
        {
            var bikestoreContext = _context.OrderItems.Include(o => o.Order).Include(o => o.Product);
            return View(await bikestoreContext.ToListAsync());
        }

        // GET: OrderItems/OrderItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrderItems/OrderItems/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: OrderItems/OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ItemId,ProductId,Quantity,ListPrice,Discount")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/OrderItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: OrderItems/OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ItemId,ProductId,Quantity,ListPrice,Discount")] OrderItem orderItem)
        {
            if (id != orderItem.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderItem.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/OrderItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrderItems/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderItems == null)
            {
                return Problem("Entity set 'BikestoreContext.OrderItems'  is null.");
            }
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
          return (_context.OrderItems?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
