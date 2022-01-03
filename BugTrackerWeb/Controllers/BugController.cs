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
            var bug = await _db.Bugs
               .Include(b => b.Project)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (bug == null) {
                return NotFound();
            }
            return View(bug);
        }

        // POST: Bug/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,Description,Fixed, Severity, Id,ProjectId")] Bug bug) {

            bug.Project = _db.Projects.Find(bug.ProjectId);

            if (ModelState.IsValid || bug.Project != null)
            {
                try {
                    Bug _bug = _db.Bugs.Find(bug.Id);
                    UpdateBug(bug, _bug);
                    _db.Update(_bug);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Project", "ProjectList", new { id = _bug.ProjectId });
                } catch (DbUpdateConcurrencyException) {
                    if (!BugExists(bug.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                
            }
          
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
            return RedirectToAction("Project", "ProjectList", new { id = bug.ProjectId });
        }

        private bool BugExists(int id) {
            return _db.Bugs.Any(e => e.Id == id);
        }

        private void UpdateBug(Bug Model, Bug bug)
        {
            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model));
            }
            if (bug == null)
            {
                return;
            }

            bug.Fixed = Model.Fixed;
            bug.Name = Model.Name;
            bug.Description = Model.Description;
            bug.Severity = Model.Severity;
        }
    }
}
