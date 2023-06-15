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
    public class StudentAttendancesController : Controller
    {
        private readonly SchoolSystemContext _context;

        public StudentAttendancesController(SchoolSystemContext context)
        {
            _context = context;
        }

        // GET: StudentAttendances
        public async Task<IActionResult> Index()
        {
            var schoolSystemContext = _context.StudentAttendances.Include(s => s.Class).Include(s => s.Subject);
            return View(await schoolSystemContext.ToListAsync());
        }

        // GET: StudentAttendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentAttendances == null)
            {
                return NotFound();
            }

            var studentAttendance = await _context.StudentAttendances
                .Include(s => s.Class)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAttendance == null)
            {
                return NotFound();
            }

            return View(studentAttendance);
        }

        // GET: StudentAttendances/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: StudentAttendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassId,SubjectId,RollNo,Status,Date")] StudentAttendance studentAttendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentAttendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", studentAttendance.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", studentAttendance.SubjectId);
            return View(studentAttendance);
        }

        // GET: StudentAttendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentAttendances == null)
            {
                return NotFound();
            }

            var studentAttendance = await _context.StudentAttendances.FindAsync(id);
            if (studentAttendance == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", studentAttendance.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", studentAttendance.SubjectId);
            return View(studentAttendance);
        }

        // POST: StudentAttendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassId,SubjectId,RollNo,Status,Date")] StudentAttendance studentAttendance)
        {
            if (id != studentAttendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentAttendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentAttendanceExists(studentAttendance.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", studentAttendance.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", studentAttendance.SubjectId);
            return View(studentAttendance);
        }

        // GET: StudentAttendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentAttendances == null)
            {
                return NotFound();
            }

            var studentAttendance = await _context.StudentAttendances
                .Include(s => s.Class)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAttendance == null)
            {
                return NotFound();
            }

            return View(studentAttendance);
        }

        // POST: StudentAttendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentAttendances == null)
            {
                return Problem("Entity set 'SchoolSystemContext.StudentAttendances'  is null.");
            }
            var studentAttendance = await _context.StudentAttendances.FindAsync(id);
            if (studentAttendance != null)
            {
                _context.StudentAttendances.Remove(studentAttendance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentAttendanceExists(int id)
        {
          return (_context.StudentAttendances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
