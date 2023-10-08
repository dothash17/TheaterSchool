using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class TeacherTimetableController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public TeacherTimetableController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: TeacherTimetable
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.TeacherTimetable.Include(t => t.Teacher).ThenInclude(t => t.PhysicalPerson).Include(t => t.Timetable);
            return View(await theaterSchoolDBContext.ToListAsync());
        }

        // GET: TeacherTimetable/Create
        public IActionResult Create()
        {
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "PhysicalPersonID", "PhysicalPerson");
            ViewData["TimetableID"] = new SelectList(_context.Timetable, "TimetableID", "TimetableID");
            return View();
        }

        // POST: TeacherTimetable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,TimetableID")] TeacherTimetable teacherTimetable)
        {
            try
            {
                _context.Add(teacherTimetable);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }

            return RedirectToAction(nameof(Index));
        }

        // GET: TeacherTimetable/Delete/5
        public async Task<IActionResult> Delete(int? teacherId, int? timetableId)
        {
            if (teacherId == null || timetableId == null || _context.TeacherTimetable == null)
            {
                return NotFound();
            }

            var teacherTimetable = await _context.TeacherTimetable
                .Include(t => t.Teacher)
                .Include(t => t.Timetable)
                .FirstOrDefaultAsync(m => m.TeacherID == teacherId && m.TimetableID == timetableId);
            if (teacherTimetable == null)
            {
                return NotFound();
            }

            return View(teacherTimetable);
        }

        // POST: TeacherTimetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int teacherId, int timetableId)
        {
            if (_context.TeacherTimetable == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.TeacherTimetable'  is null.");
            }
            var teacherTimetable = await _context.TeacherTimetable.FindAsync(teacherId, timetableId);
            if (teacherTimetable != null)
            {
                _context.TeacherTimetable.Remove(teacherTimetable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherTimetableExists(int teacherId, int timetableId)
        {
          return (_context.TeacherTimetable?.Any(e => e.TeacherID == teacherId && e.TimetableID == timetableId)).GetValueOrDefault();
        }
    }
}
