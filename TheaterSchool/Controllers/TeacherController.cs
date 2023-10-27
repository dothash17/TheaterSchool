using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TheaterSchool.Models;
using TheaterSchool.Models.Data;

namespace TheaterSchool.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TheaterSchoolDBContext _context;

        public TeacherController(TheaterSchoolDBContext context)
        {
            _context = context;
        }

        // GET: Teacher
        public async Task<IActionResult> Index()
        {
            var theaterSchoolDBContext = _context.Teacher.Include(t => t.PhysicalPerson);
            return View(await theaterSchoolDBContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString, bool? isMale, bool? isFemale)
        {
            var query = _context.Teacher.Include(s => s.PhysicalPerson).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(s =>
                    s.PhysicalPerson.LastName.Contains(searchString) ||
                    s.PhysicalPerson.FirstName.Contains(searchString) ||
                    s.PhysicalPerson.SecondName.Contains(searchString));
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

        // GET: Teacher/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .Include(t => t.PhysicalPerson)
                .FirstOrDefaultAsync(m => m.PhysicalPersonID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // GET: Teacher/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["PhysicalPersonID"] = new SelectList(_context.PhysicalPersons, "ID", "ID");
            return View();
        }

        // POST: Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("PhysicalPersonID,Position,Experience")] Teacher teacher)
        {
            try
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { return View(ErrorConstants.InvalidInputError); throw; }
            return RedirectToAction(nameof(Index));
        }

        // GET: Teacher/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            ViewData["PhysicalPersonID"] = new SelectList(_context.PhysicalPersons, "ID", "ID", teacher.PhysicalPersonID);
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("PhysicalPersonID,Position,Experience")] Teacher teacher)
        {
            if (id != teacher.PhysicalPersonID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(teacher);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(teacher.PhysicalPersonID))
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

        // GET: Teacher/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teacher == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teacher
                .Include(t => t.PhysicalPerson)
                .FirstOrDefaultAsync(m => m.PhysicalPersonID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teacher == null)
            {
                return Problem("Entity set 'TheaterSchoolDBContext.Teacher'  is null.");
            }
            var teacher = await _context.Teacher.FindAsync(id);
            if (teacher != null)
            {
                _context.Teacher.Remove(teacher);
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

        private bool TeacherExists(int id)
        {
          return (_context.Teacher?.Any(e => e.PhysicalPersonID == id)).GetValueOrDefault();
        }
    }
}
