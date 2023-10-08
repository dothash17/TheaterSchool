using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class SubjectController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public SubjectController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
              return _context.Subject != null ? 
                          View(await _context.Subject.ToListAsync()) :
                          Problem("Entity set 'TheaterSchoolDBContext.Subject'  is null.");
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subject == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.SubjectID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("SubjectID,SubjectName,Duration")] Subject subject)
        {
            try
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        // GET: Subject/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subject == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectID,SubjectName,Duration")] Subject subject)
        {
            if (id != subject.SubjectID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(subject);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(subject.SubjectID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        // GET: Subject/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subject == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .FirstOrDefaultAsync(m => m.SubjectID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subject == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.Subject'  is null.");
            }
            var subject = await _context.Subject.FindAsync(id);
            if (subject != null)
            {
                _context.Subject.Remove(subject);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return View(ErrorConstants.InvalidInputError);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
          return (_context.Subject?.Any(e => e.SubjectID == id)).GetValueOrDefault();
        }
    }
}
