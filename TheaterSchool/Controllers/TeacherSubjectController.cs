using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class TeacherSubjectController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public TeacherSubjectController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: TeacherSubject
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.TeacherSubject.Include(t => t.Subject).Include(t => t.Teacher).ThenInclude(t => t.PhysicalPerson);
            return View(await theaterSchoolDBContext.ToListAsync());
        }

        // GET: TeacherSubject/Create
        public IActionResult Create()
        {
            ViewData["SubjectID"] = new SelectList(_context.Subject, "SubjectID", "SubjectID");
            ViewData["TeacherID"] = new SelectList(_context.Teacher, "PhysicalPersonID", "PhysicalPersonID");
            return View();
        }

        // POST: TeacherSubject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,SubjectID")] TeacherSubject teacherSubject)
        {
            try
            {
                _context.Add(teacherSubject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }

            return RedirectToAction(nameof(Index));
        }

        // GET: TeacherSubject/Delete/5
        public async Task<IActionResult> Delete(int? teacherId, int? subjectId)
        {
            if (teacherId == null || subjectId == null || _context.TeacherSubject == null)
            {
                return NotFound();
            }

            var teacherSubject = await _context.TeacherSubject
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeacherID == teacherId && m.SubjectID == subjectId);
            if (teacherSubject == null)
            {
                return NotFound();
            }

            return View(teacherSubject);
        }

        // POST: TeacherSubject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int teacherId, int subjectId)
        {
            if (_context.TeacherSubject == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.TeacherSubject'  is null.");
            }
            var teacherSubject = await _context.TeacherSubject.FindAsync(teacherId, subjectId);
            if (teacherSubject != null)
            {
                _context.TeacherSubject.Remove(teacherSubject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherSubjectExists(int teacherId, int subjectId)
        {
          return (_context.TeacherSubject?.Any(e => e.TeacherID == teacherId && e.SubjectID == subjectId)).GetValueOrDefault();
        }
    }
}
