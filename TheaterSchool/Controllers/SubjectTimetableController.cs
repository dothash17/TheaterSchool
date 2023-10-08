using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class SubjectTimetableController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public SubjectTimetableController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: SubjectTimetable
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.SubjectTimetable.Include(s => s.Subject).Include(s => s.Timetable);
            return View(await theaterSchoolDBContext.ToListAsync());
        }

        // GET: SubjectTimetable/Create
        public IActionResult Create()
        {
            ViewData["SubjectID"] = new SelectList(_context.Subject, "SubjectID", "SubjectID");
            ViewData["TimetableID"] = new SelectList(_context.Timetable, "TimetableID", "TimetableID");
            return View();
        }

        // POST: SubjectTimetable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectID,TimetableID")] SubjectTimetable subjectTimetable)
        {
            try
            {
                _context.Add(subjectTimetable);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }

            return RedirectToAction(nameof(Index));
        }

        // GET: SubjectTimetable/Delete/5
        public async Task<IActionResult> Delete(int? subjectId, int? timetableId)
        {
            if (subjectId == null || timetableId == null || _context.SubjectTimetable == null)
            {
                return NotFound();
            }

            var subjectTimetable = await _context.SubjectTimetable
                .Include(s => s.Subject)
                .Include(s => s.Timetable)
                .FirstOrDefaultAsync(m => m.SubjectID == subjectId && m.TimetableID == timetableId);
            if (subjectTimetable == null)
            {
                return NotFound();
            }

            return View(subjectTimetable);
        }

        // POST: SubjectTimetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int subjectId, int timetableId)
        {
            if (_context.SubjectTimetable == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.SubjectTimetable'  is null.");
            }
            var subjectTimetable = await _context.SubjectTimetable.FindAsync(subjectId, timetableId);
            if (subjectTimetable != null)
            {
                _context.SubjectTimetable.Remove(subjectTimetable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectTimetableExists(int subjectId, int timetableId)
        {
          return (_context.SubjectTimetable?.Any(e => e.SubjectID == subjectId && e.TimetableID == timetableId)).GetValueOrDefault();
        }
    }
}
