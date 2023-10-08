using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class StudentController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public StudentController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.Student.Include(s => s.PhysicalPerson);
            return View(await theaterSchoolDBContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString, int? courseFilter, bool? isMale, bool? isFemale)
        {
            if (string.IsNullOrWhiteSpace(searchString) && courseFilter == null && isMale == null && isFemale == null)
            {
                return RedirectToAction("Index");
            }

            var query = _context.Student.Include(s => s.PhysicalPerson).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(s =>
                    s.PhysicalPerson.LastName.Contains(searchString) ||
                    s.PhysicalPerson.FirstName.Contains(searchString) ||
                    s.PhysicalPerson.SecondName.Contains(searchString));
            }

            if (courseFilter != null)
            {
                query = query.Where(s => s.Course == courseFilter);
            }

            if (isMale == true)
            {
                query = query.Where(s => s.PhysicalPerson.Sex == true);
            }

            if (isFemale == true)
            {
                query = query.Where(s => s.PhysicalPerson.Sex == false);
            }

            var appDbContext = await query.ToListAsync();

            return View("Index", appDbContext);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.PhysicalPerson)
                .FirstOrDefaultAsync(m => m.PhysicalPersonID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["PhysicalPersonID"] = new SelectList(_context.PhysicalPersons, "ID", "ID");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("PhysicalPersonID,Course,Group")] Student student)
        {
            try
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["PhysicalPersonID"] = new SelectList(_context.PhysicalPersons, "ID", "Address", student.PhysicalPersonID);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PhysicalPersonID,Course,Group")] Student student)
        {
            if (id != student.PhysicalPersonID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.PhysicalPersonID))
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

        // GET: Student/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.PhysicalPerson)
                .FirstOrDefaultAsync(m => m.PhysicalPersonID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
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

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.PhysicalPersonID == id)).GetValueOrDefault();
        }
    }
}
