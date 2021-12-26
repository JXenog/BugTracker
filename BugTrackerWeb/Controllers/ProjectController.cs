using BugTrackerWeb.Data;
using BugTrackerWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWeb.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProjectController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Project> objProjectList = _db.Projects.OrderBy(x => x.DisplayOrder);
            return View(objProjectList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var projectFromDb = _db.Projects.Find(id);
            if(projectFromDb == null)
            {
                return NotFound();
            }

            return View(projectFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var projectFromDb = _db.Projects.Find(id);
            if (projectFromDb == null)
            {
                return NotFound();
            }

            _db.Projects.Remove(projectFromDb);
            _db.SaveChanges();
            TempData["success"] = "Project deleted successfully.";
            return RedirectToAction("Index");
 
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project obj)
        {
            if (ModelState.IsValid && obj != null)
            {
                var entity = _db.Projects.Find(obj.Id);
                if(entity != null)
                {
                    entity.Title = obj.Title;
                    entity.DisplayOrder = obj.DisplayOrder;

                    _db.Projects.Update(entity);
                    _db.SaveChanges();
                    TempData["success"] = "Project updated successfully.";
                    return RedirectToAction("Index");
                } 
            }

            return View(obj);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project obj)
        {
            if(ModelState.IsValid)
            {
                _db.Projects.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Project created successfully.";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
