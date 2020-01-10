﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ffdraftday.Models;
using ffdraftday.Repos;
using Microsoft.AspNetCore.Mvc;

namespace ffdraftday.Controllers
{
    public class KeepersController : Controller
    {

        private Repo _repo;
        public KeepersController(Repo repo)
        {
            _repo = repo;
        }

        public IActionResult Edit(int id)
        {
            var keeper = _repo.keepers.Get(id);
            return View(keeper);
        }

        [HttpPost]
        public IActionResult Edit(Keeper keeper)
        {
            if (ModelState.IsValid)
            {
                _repo.keepers.Save(keeper);
                return RedirectToAction("Details", "Teams", new { id = keeper.TeamId });
            }
            keeper.Team = _repo.teams.Get(keeper.TeamId);
            keeper.Player = _repo.players.Get(keeper.PlayerId ?? 0);
            return View(keeper);
        }

    }
}