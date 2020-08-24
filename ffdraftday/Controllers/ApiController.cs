using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ffdraftday.Controllers
{
    public class ApiController : Controller
    {
        private Repos.Repo _repo;

        public ApiController(Repos.Repo repo)
        {
            _repo = repo;
        }

        [HttpGet("api/teams/{draftId}")]

        public async Task<JsonResult> Teams(int draftId)
        {


            var teams = _repo.teams.List(draftId);
            return Json(teams);
        }

        public async Task<JsonResult> Players(string searchText = "")
        {
            var players = _repo.players.PlayerSelectList(searchText);
            return Json(players);
        }
    }
}