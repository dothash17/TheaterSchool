using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class StudentPerformanceController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public StudentPerformanceController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: StudentPerformance
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.StudentPerformance.Include(s => s.Performance).Include(s => s.Student).ThenInclude(s => s.PhysicalPerson);            
            return View(await theaterSchoolDBContext.ToListAsync());
        }

        // GET: StudentPerformance/Create
        public IActionResult Create()
        {
            ViewData["PerformanceID"] = new SelectList(_context.Performance, "PerformanceID", "PerformanceID");
            ViewData["StudentID"] = new SelectList(_context.Student, "PhysicalPersonID", "PhysicalPersonID");
            return View();
        }

        // POST: StudentPerformance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,PerformanceID")] StudentPerformance studentPerformance)
        {
            try
            {
                _context.Add(studentPerformance);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }

            return RedirectToAction(nameof(Index));
        }

        // GET: StudentPerformance/Delete/5
        public async Task<IActionResult> Delete(int? studentId, int? performanceId)
        {
            if (studentId == null || performanceId == null || _context.StudentPerformance == null)
            {
                return NotFound();
            }

            var studentPerformance = await _context.StudentPerformance
                .Include(s => s.Performance)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.StudentID == studentId && m.PerformanceID == performanceId);
            if (studentPerformance == null)
            {
                return NotFound();
            }

            return View(studentPerformance);
        }

        // POST: StudentPerformance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int studentId, int performanceId)
        {
            if (_context.StudentPerformance == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.StudentPerformance'  is null.");
            }
            var studentPerformance = await _context.StudentPerformance.FindAsync(studentId, performanceId);
            if (studentPerformance != null)
            {
                _context.StudentPerformance.Remove(studentPerformance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentPerformanceExists(int studentId, int performanceId)
        {
          return (_context.StudentPerformance?.Any(e => e.StudentID == studentId && e.PerformanceID == performanceId)).GetValueOrDefault();
        }
    }
}
