using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class TimetableController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public TimetableController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: Timetable
        public async Task<IActionResult> Index()
        {
              return _context.Timetable != null ? 
                          View(await _context.Timetable.ToListAsync()) :
                          Problem("Entity set 'TheaterSchoolDBContext.Timetable'  is null.");
        }

        // GET: Timetable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Timetable == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetable
                .FirstOrDefaultAsync(m => m.TimetableID == id);
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        // GET: Timetable/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Timetable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("TimetableID,DayOfTheWeek,PeriodNumber,ClassRoom")] Timetable timetable)
        {
            try
            {
                _context.Add(timetable);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        // GET: Timetable/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Timetable == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetable.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }
            return View(timetable);
        }

        // POST: Timetable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("TimetableID,DayOfTheWeek,PeriodNumber,ClassRoom")] Timetable timetable)
        {
            if (id != timetable.TimetableID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(timetable);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimetableExists(timetable.TimetableID))
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

        // GET: Timetable/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Timetable == null)
            {
                return NotFound();
            }

            var timetable = await _context.Timetable
                .FirstOrDefaultAsync(m => m.TimetableID == id);
            if (timetable == null)
            {
                return NotFound();
            }

            return View(timetable);
        }

        // POST: Timetable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Timetable == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.Timetable'  is null.");
            }
            var timetable = await _context.Timetable.FindAsync(id);
            if (timetable != null)
            {
                _context.Timetable.Remove(timetable);
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

        private bool TimetableExists(int id)
        {
          return (_context.Timetable?.Any(e => e.TimetableID == id)).GetValueOrDefault();
        }
    }
}
