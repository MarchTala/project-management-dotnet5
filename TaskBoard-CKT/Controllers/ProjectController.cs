using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard_CKT.Data;
using TaskBoard_CKT.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskBoard_CKT.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProjectController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<Project> objList = _db.Projects;
            return View(objList);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST-Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project obj)
        {
            _db.Projects.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET Delete
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var obj = _db.Projects.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST Delete
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _db.Projects.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Projects.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET Update
        [Authorize]
        public IActionResult Update(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var obj = _db.Projects.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST Update
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Project obj)
        {
            if (ModelState.IsValid)
            {
                _db.Projects.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }
    }
}
