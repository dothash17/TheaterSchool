using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class PhysicalPersonController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public PhysicalPersonController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: PhysicalPerson
        public async Task<IActionResult> Index()
        {
              return _context.PhysicalPersons != null ? 
                          View(await _context.PhysicalPersons.ToListAsync()) :
                          Problem("Entity set 'TheaterSchoolDBContext.PhysicalPersons'  is null.");
        }

        // GET: PhysicalPerson/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhysicalPersons == null)
            {
                return NotFound();
            }

            var physicalPersons = await _context.PhysicalPersons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (physicalPersons == null)
            {
                return NotFound();
            }

            return View(physicalPersons);
        }

        // GET: PhysicalPerson/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhysicalPerson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,SecondName,DateOfBirth,Phone,Sex,Address")] PhysicalPersons physicalPersons)
        {
            int currentYear = DateTime.Now.Year;
            int minimumYear = 1950;
            if (physicalPersons.DateOfBirth.Year > currentYear || physicalPersons.DateOfBirth.Year < minimumYear)
            {
                ModelState.AddModelError("DateOfBirth", "Год рождения должен быть между " + minimumYear + " и " + currentYear + ".");
                return View(physicalPersons);
            }

            try
            {
                _context.Add(physicalPersons);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        // GET: PhysicalPerson/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhysicalPersons == null)
            {
                return NotFound();
            }

            var physicalPersons = await _context.PhysicalPersons.FindAsync(id);
            if (physicalPersons == null)
            {
                return NotFound();
            }
            return View(physicalPersons);
        }

        // POST: PhysicalPerson/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,SecondName,DateOfBirth,Phone,Sex,Address")] PhysicalPersons physicalPersons)
        {
            if (id != physicalPersons.ID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(physicalPersons);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhysicalPersonsExists(physicalPersons.ID))
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

        // GET: PhysicalPerson/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhysicalPersons == null)
            {
                return NotFound();
            }

            var physicalPersons = await _context.PhysicalPersons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (physicalPersons == null)
            {
                return NotFound();
            }

            return View(physicalPersons);
        }

        // POST: PhysicalPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PhysicalPersons == null)
            {
                return Problem("Entity set 'AppDbContext.PhysicalPersons'  is null.");
            }
            var physicalPerson = await _context.PhysicalPersons.FindAsync(id);
            if (physicalPerson != null)
            {
                _context.PhysicalPersons.Remove(physicalPerson);
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

        private bool PhysicalPersonsExists(int id)
        {
          return (_context.PhysicalPersons?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
