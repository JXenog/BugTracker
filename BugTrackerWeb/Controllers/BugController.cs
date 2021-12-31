#nullable disable
using BugTrackerWeb.Data;
using BugTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWeb.Controllers {
    public class BugController : Controller {
        private readonly ApplicationDbContext _db;

        public BugController(ApplicationDbContext db) {
            _db = db;
        }

        // GET: Bug
        public async Task<IActionResult> Index() {
            var applicationDbContext = _db.Bugs.Include(b => b.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bug/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bug = await _db.Bugs
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null) {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bug/Create
        [HttpGet]
        public IActionResult Create(int? ProjectId) {
            if (ProjectId != null) {
                var id = ProjectId;
                ViewData["ProjectId"] = id;
                return View();
            }
            return NotFound();
        }

        // POST: Bug/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bug bug) {
            var id = bug.ProjectId;
            bug.Project = _db.Projects.Find(id);

            if (ModelState.IsValid || bug.Project != null) {
                var _bug = new Bug() {
                    Project = bug.Project,
                    Description = bug.Description,
                    Fixed = bug.Fixed,
                    Id = bug.Id,
                    Name = bug.Name,
                    Severity = bug.Severity
                };
                _db.Bugs.Add(_bug);
                await _db.SaveChangesAsync();
                TempData["success"] = "Bug created successfully.";
                return RedirectToAction("Project", "ProjectList", new { id = _bug.ProjectId });
            }
            TempData["error"] = "Failed to create bug.";
            ViewData["ProjectId"] = bug.ProjectId;
            return View(bug);
        }

        // GET: Bug/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bug = await _db.Bugs.FindAsync(id);
            if (bug == null) {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_db.Projects, "Id", "Title", bug.ProjectId);
            return View(bug);
        }

        // POST: Bug/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Fixed,ProjectId,Id,CreatedDate,UpdateDate")] Bug bug) {
            if (id != bug.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _db.Update(bug);
                    await _db.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!BugExists(bug.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_db.Projects, "Id", "Title", bug.ProjectId);
            return View(bug);
        }

        // GET: Bug/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var bug = await _db.Bugs
                .Include(b => b.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null) {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bug/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var bug = await _db.Bugs.FindAsync(id);
            _db.Bugs.Remove(bug);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id) {
            return _db.Bugs.Any(e => e.Id == id);
        }
    }
}
