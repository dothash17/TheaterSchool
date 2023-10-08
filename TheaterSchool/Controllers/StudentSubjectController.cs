using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class StudentSubjectController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public StudentSubjectController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: StudentSubject
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.StudentSubject.Include(s => s.Student).ThenInclude(s => s.PhysicalPerson).Include(s => s.Subject);
            return View(await theaterSchoolDBContext.ToListAsync());
        }
       
        // GET: StudentSubject/Create
        public IActionResult Create()
        {
            ViewData["StudentID"] = new SelectList(_context.Student, "PhysicalPersonID", "PhysicalPersonID");
            ViewData["SubjectID"] = new SelectList(_context.Subject, "SubjectID", "SubjectID");
            return View();
        }

        // POST: StudentSubject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,SubjectID")] StudentSubject studentSubject)
        {
            try
            {
                _context.Add(studentSubject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }

            return RedirectToAction(nameof(Index));
        }

        // GET: StudentSubject/Delete/5
        public async Task<IActionResult> Delete(int? studentId, int? subjectId)
        {
            if (studentId == null || subjectId == null || _context.StudentSubject == null)
            {
                return NotFound();
            }

            var studentSubject = await _context.StudentSubject
                .Include(s => s.Student)
                .Include(s => s.Subject)
                .FirstOrDefaultAsync(m => m.StudentID == studentId && m.SubjectID == subjectId);
            if (studentSubject == null)
            {
                return NotFound();
            }

            return View(studentSubject);
        }

        // POST: StudentSubject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int studentId, int subjectId)
        {
            if (_context.StudentSubject == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.StudentSubject'  is null.");
            }
            var studentSubject = await _context.StudentSubject.FindAsync(studentId, subjectId);
            if (studentSubject != null)
            {
                _context.StudentSubject.Remove(studentSubject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentSubjectExists(int studentId, int subjectId)
        {
          return (_context.StudentSubject?.Any(e => e.StudentID == studentId && e.SubjectID == subjectId)).GetValueOrDefault();
        }
    }
}
