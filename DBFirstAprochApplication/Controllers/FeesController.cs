using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBFirstLayer;
using DBFirstLayer.Models;

namespace DBFirstAprochApplication.Controllers
{
    public class FeesController : Controller
    {
        private readonly SchoolSystemContext _context;

        public FeesController(SchoolSystemContext context)
        {
            _context = context;
        }

        // GET: Fees
        public async Task<IActionResult> Index()
        {
            var schoolSystemContext = _context.Fees.Include(f => f.Class);
            return View(await schoolSystemContext.ToListAsync());
        }

        // GET: Fees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees
                .Include(f => f.Class)
                .FirstOrDefaultAsync(m => m.FeesId == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // GET: Fees/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FeesId,ClassId,FeesAmount")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", fee.ClassId);
            return View(fee);
        }

        // GET: Fees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees.FindAsync(id);
            if (fee == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", fee.ClassId);
            return View(fee);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeesId,ClassId,FeesAmount")] Fee fee)
        {
            if (id != fee.FeesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeExists(fee.FeesId))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", fee.ClassId);
            return View(fee);
        }

        // GET: Fees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fee = await _context.Fees
                .Include(f => f.Class)
                .FirstOrDefaultAsync(m => m.FeesId == id);
            if (fee == null)
            {
                return NotFound();
            }

            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fees == null)
            {
                return Problem("Entity set 'SchoolSystemContext.Fees'  is null.");
            }
            var fee = await _context.Fees.FindAsync(id);
            if (fee != null)
            {
                _context.Fees.Remove(fee);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeExists(int id)
        {
          return (_context.Fees?.Any(e => e.FeesId == id)).GetValueOrDefault();
        }
    }
}
