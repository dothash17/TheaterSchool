using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class PerformanceController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public PerformanceController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: Performances
        public async Task<IActionResult> Index()
        {
              return _context.Performance != null ? 
                          View(await _context.Performance.ToListAsync()) :
                          Problem("Entity set 'TheaterSchoolDBContext.Performance'  is null.");
        }

        // GET: Performances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Performance == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .FirstOrDefaultAsync(m => m.PerformanceID == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // GET: Performances/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("PerformanceID,PerformanceName,Description,Genre,Duration,Location,Requisite")] Performance performance)
        {
            try
            {
                _context.Add(performance);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        // GET: Performances/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Performance == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PerformanceID,PerformanceName,Description,Genre,Duration,Location,Requisite")] Performance performance)
        {
            if (id != performance.PerformanceID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(performance);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceExists(performance.PerformanceID))
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

        // GET: Performances/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Performance == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .FirstOrDefaultAsync(m => m.PerformanceID == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Performance == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.Performance'  is null.");
            }
            var performance = await _context.Performance.FindAsync(id);
            if (performance != null)
            {
                _context.Performance.Remove(performance);
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

        private bool PerformanceExists(int id)
        {
          return (_context.Performance?.Any(e => e.PerformanceID == id)).GetValueOrDefault();
        }
    }
}
