using BugTrackerWeb.Data;
using BugTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWeb.Controllers {
    public class ProjectListController : Controller {
        private readonly ApplicationDbContext _db;

        public ProjectListController(ApplicationDbContext db) {
            _db = db;
        }

        public async Task<IActionResult> Index() {
            IEnumerable<Project> objProjectList = await _db.Projects.OrderBy(x => x.Title).ToListAsync();
            return View(objProjectList);
        }

        //GET
        public IActionResult Create() {
            return View();
        }

        //GET
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || id == 0) {
                return NotFound();
            }
            var projectFromDb = await _db.Projects.FindAsync(id);
            if (projectFromDb == null) {
                return NotFound();
            }

            return View(projectFromDb);
        }

        // GET
        public async Task<IActionResult> Project(int? id) {
            if (id == null) {
                return NotFound();
            }

            var project = await _db.Projects.Include(b => b.Bugs).FirstOrDefaultAsync(a => a.Id == id);

            if (project == null) {
                return NotFound();
            }

            return View(project);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || id == 0) {
                TempData["error"] = "Failed to delete project.";
                return NotFound();
            }
            var projectFromDb = await _db.Projects.FindAsync(id);
            if (projectFromDb == null) {
                TempData["error"] = "Failed to delete project.";
                return NotFound();
            }

            _db.Projects.Remove(projectFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Project deleted successfully.";
            return RedirectToAction("Index");

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Project obj) {
            if (ModelState.IsValid) {
                var entity = await _db.Projects.FindAsync(obj.Id);
                if (entity != null) {
                    entity.Title = obj.Title;

                    _db.Projects.Update(entity);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "Project updated successfully.";
                    return RedirectToAction("Index");
                }
            }

            TempData["error"] = "Failed to update project.";
            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project obj) {
            if (ModelState.IsValid) {
                _db.Projects.Add(obj);
                await _db.SaveChangesAsync();
                TempData["success"] = "Project created successfully.";

                return RedirectToAction("Index");
            }

            TempData["error"] = "Failed to create project.";
            return View(obj);
        }
    }
}
