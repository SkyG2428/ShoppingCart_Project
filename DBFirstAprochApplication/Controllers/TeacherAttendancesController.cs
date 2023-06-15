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
    public class TeacherAttendancesController : Controller
    {
        private readonly SchoolSystemContext _context;

        public TeacherAttendancesController(SchoolSystemContext context)
        {
            _context = context;
        }

        // GET: TeacherAttendances
        public async Task<IActionResult> Index()
        {
            var schoolSystemContext = _context.TeacherAttendances.Include(t => t.Teacher);
            return View(await schoolSystemContext.ToListAsync());
        }

        // GET: TeacherAttendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherAttendances == null)
            {
                return NotFound();
            }

            var teacherAttendance = await _context.TeacherAttendances
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherAttendance == null)
            {
                return NotFound();
            }

            return View(teacherAttendance);
        }

        // GET: TeacherAttendances/Create
        public IActionResult Create()
        {
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId");
            return View();
        }

        // POST: TeacherAttendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeacherId,Status,Date")] TeacherAttendance teacherAttendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherAttendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", teacherAttendance.TeacherId);
            return View(teacherAttendance);
        }

        // GET: TeacherAttendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherAttendances == null)
            {
                return NotFound();
            }

            var teacherAttendance = await _context.TeacherAttendances.FindAsync(id);
            if (teacherAttendance == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", teacherAttendance.TeacherId);
            return View(teacherAttendance);
        }

        // POST: TeacherAttendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeacherId,Status,Date")] TeacherAttendance teacherAttendance)
        {
            if (id != teacherAttendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherAttendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherAttendanceExists(teacherAttendance.Id))
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
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", teacherAttendance.TeacherId);
            return View(teacherAttendance);
        }

        // GET: TeacherAttendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherAttendances == null)
            {
                return NotFound();
            }

            var teacherAttendance = await _context.TeacherAttendances
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherAttendance == null)
            {
                return NotFound();
            }

            return View(teacherAttendance);
        }

        // POST: TeacherAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherAttendances == null)
            {
                return Problem("Entity set 'SchoolSystemContext.TeacherAttendances'  is null.");
            }
            var teacherAttendance = await _context.TeacherAttendances.FindAsync(id);
            if (teacherAttendance != null)
            {
                _context.TeacherAttendances.Remove(teacherAttendance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherAttendanceExists(int id)
        {
          return (_context.TeacherAttendances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
