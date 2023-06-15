using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeStoreDataBaseLibrary;
using BikeStoreDataBaseLibrary.Entity;

namespace BikeStoreCrudeEFDBFirstApproch.Areas.Stocks.Controllers
{
    [Area("Stocks")]
    public class StocksController : Controller
    {
        private readonly BikestoreContext _context;

        public StocksController(BikestoreContext context)
        {
            _context = context;
        }

        // GET: Stocks/Stocks
        public async Task<IActionResult> Index()
        {
            var bikestoreContext = _context.Stocks.Include(s => s.Product).Include(s => s.Store);
            return View(await bikestoreContext.ToListAsync());
        }

        // GET: Stocks/Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stocks == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Product)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Stocks/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName");
            return View();
        }

        // POST: Stocks/Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,ProductId,Quantity")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", stock.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", stock.StoreId);
            return View(stock);
        }

        // GET: Stocks/Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stocks == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", stock.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", stock.StoreId);
            return View(stock);
        }

        // POST: Stocks/Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,ProductId,Quantity")] Stock stock)
        {
            if (id != stock.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.StoreId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", stock.ProductId);
            ViewData["StoreId"] = new SelectList(_context.Stores, "StoreId", "StoreName", stock.StoreId);
            return View(stock);
        }

        // GET: Stocks/Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stocks == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Product)
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stocks/Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stocks == null)
            {
                return Problem("Entity set 'BikestoreContext.Stocks'  is null.");
            }
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
          return (_context.Stocks?.Any(e => e.StoreId == id)).GetValueOrDefault();
        }
    }
}
