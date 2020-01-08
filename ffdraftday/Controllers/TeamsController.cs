using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ffdraftday.Models;
using Microsoft.AspNetCore.Mvc;
using ffdraftday.Repos;

namespace ffdraftday.Controllers
{
    public class TeamsController : Controller
    {
        private ffdraftdayContext _db;
        private Repo _repo;
        public TeamsController(ffdraftdayContext db, Repo repo)
        {
            _db = db;
            _repo = repo;
        }
        
        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            var vm = _repo.teams.GetViewModel(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            var team = _repo.teams.Get(id);
            if (team == null) return NotFound();
            return View(team);
        }

        [HttpPost]
        public IActionResult Edit(Team changes)
        {
            if (ModelState.IsValid)
            {
                _repo.teams.Update(changes);
                return RedirectToAction("Details", new { id = changes.Id });
            }
            return View(changes);
        }

        public IActionResult Move(int draftId)
        {
            var teams = _repo.teams.List(draftId);
            ViewBag.Draft = _repo.drafts.Get(draftId);
            return View(teams);
        }

        [HttpPost]
        public IActionResult Move(int id, string direction)
        {
            if (id == 0) return NotFound("No team id passed");
            var team = _repo.teams.Get(id);
            if (team == null) return NotFound("invalid team");
            _repo.teams.Move(id, (direction == "Move Up" ? -1 : 1));
            return RedirectToAction("Move", new { draftId = team.DraftId });
        }
    }
}