using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BikeStoreDataBaseLibrary;
using BikeStoreDataBaseLibrary.Entity;

namespace BikeStoreCrudeEFDBFirstApproch.Areas.Stores.Controllers
{
    [Area("Stores")]
    public class StoresController : Controller
    {
        private readonly BikestoreContext _context;

        public StoresController(BikestoreContext context)
        {
            _context = context;
        }

        // GET: Stores/Stores
        public async Task<IActionResult> Index()
        {
            var stor=_context.Stores.ToList();
            return View(stor);

              //return _context.Stores != null ? 
              //            View(await _context.Stores.ToListAsync()) :
              //            Problem("Entity set 'BikestoreContext.Stores'  is null.");
        }

        // GET: Stores/Stores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // GET: Stores/Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stores/Stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        // GET: Stores/Stores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        // POST: Stores/Stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StoreId,StoreName,Phone,Email,Street,City,State,ZipCode")] Store store)
        {
            if (id != store.StoreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(store);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreExists(store.StoreId))
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
            return View(store);
        }

        // GET: Stores/Stores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stores == null)
            {
                return NotFound();
            }

            var store = await _context.Stores
                .FirstOrDefaultAsync(m => m.StoreId == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        // POST: Stores/Stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stores == null)
            {
                return Problem("Entity set 'BikestoreContext.Stores'  is null.");
            }
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
          return (_context.Stores?.Any(e => e.StoreId == id)).GetValueOrDefault();
        }
    }
}
