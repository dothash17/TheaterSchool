using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class TeacherPerformanceController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public TeacherPerformanceController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: TeacherPerformance
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.TeacherPerformance.Include(t => t.Performance).Include(t => t.Teacher).ThenInclude(t => t.PhysicalPerson);
            return View(await theaterSchoolDBContext.ToListAsync());
        }

        // GET: TeacherPerformance/Create
        public IActionResult Create()
        {
            ViewData["PerformanceID"] = new SelectList(_context.Performance, "PerformanceID", "PerformanceID");
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "PhysicalPersonID", "PhysicalPersonID");
            return View();
        }

        // POST: TeacherPerformance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,PerformanceID")] TeacherPerformance teacherPerformance)
        {
            try
            {
                _context.Add(teacherPerformance);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }

            return RedirectToAction(nameof(Index));
        }

        // GET: TeacherPerformance/Delete/5
        public async Task<IActionResult> Delete(int? teacherId, int? performanceId)
        {
            if (teacherId == null || performanceId == null || _context.TeacherPerformance == null)
            {
                return NotFound();
            }

            var teacherPerformance = await _context.TeacherPerformance
                .Include(t => t.Performance)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherID == teacherId && m.PerformanceID == performanceId);
            if (teacherPerformance == null)
            {
                return NotFound();
            }

            return View(teacherPerformance);
        }

        // POST: TeacherPerformance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int teacherId, int performanceId)
        {
            if (_context.TeacherPerformance == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.TeacherPerformance'  is null.");
            }
            var teacherPerformance = await _context.TeacherPerformance.FindAsync(teacherId, performanceId);
            if (teacherPerformance != null)
            {
                _context.TeacherPerformance.Remove(teacherPerformance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherPerformanceExists(int teacherId, int performanceId)
        {
          return (_context.TeacherPerformance?.Any(e => e.TeacherID == teacherId && e.PerformanceID == performanceId)).GetValueOrDefault();
        }
    }
}
