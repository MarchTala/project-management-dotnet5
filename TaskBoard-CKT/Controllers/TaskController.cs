using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoard_CKT.Classes;
using TaskBoard_CKT.Data;
using TaskBoard_CKT.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskBoard_CKT.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TaskController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult List(Guid projectId)
        {
            List<Task> taskList = _db.Tasks.Where(t => t.ProjectId == projectId).ToList();

            var project = _db.Projects.Where(p => p.Id == projectId).Select(p => new { name = p.Name }).FirstOrDefault();

            if (project == null)
            {
                return NotFound();
            }

            ProjectDetails projectDetails = new ProjectDetails() {
                ProjectGUID = projectId,
                ProjectName = project.name,
                ProjectTaskList = taskList
            };

            ViewData["ProjectDetails"] = projectDetails;

            return View();
        }

        [Authorize]
        public IActionResult Detail(Guid taskId)
        {
            if (taskId == Guid.Empty)
            {
                return NotFound();
            }

            var obj = _db.Tasks.Find(taskId);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [Authorize]
        public IActionResult Create(Guid projectId)
        {
            ViewBag.ProjectId = projectId;
            return View();
        }

        // POST-Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task obj)
        {
            _db.Tasks.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("List", "Task", new { projectId = obj.ProjectId });
        }

        //GET Delete
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var obj = _db.Tasks.Find(id);
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
            var obj = _db.Tasks.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Tasks.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("List", "Task", new { projectId = obj.ProjectId });
        }

        //GET Update
        [Authorize]
        public IActionResult Update(Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
            {
                return NotFound();
            }

            var obj = _db.Tasks.Find(id);
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
        public IActionResult Update(Task obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("List", "Task", new { projectId = obj.ProjectId });
            }

            return View(obj);
        }
    }
}
