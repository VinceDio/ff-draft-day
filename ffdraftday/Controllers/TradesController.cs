using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ffdraftday.Repos;
using ffdraftday.Models;
using ffdraftday.ViewModels;

namespace ffdraftday.Controllers
{
    public class TradesController : Controller
    {
        private Repo _repo;
        public TradesController(Repo repo)
        {
            _repo = repo;
        }
        public IActionResult Create(int teamId)
        {
            var team = _repo.teams.Get(teamId);
            ViewBag.TeamList = _repo.teams.TeamList(team.DraftId);
            var trade = new Trade { Team1Id = teamId, Team1 = team, DraftId = team.DraftId };
            return View(trade);
        }

        [HttpPost]
        public IActionResult Create(Trade trade)
        {
            
            trade.Team1 = _repo.teams.Get(trade.Team1Id);
            _repo.trades.Add(trade);
            return RedirectToAction("Edit", new { id = trade.Id });
        }

        public IActionResult Edit(int id)
        {
            var vm = _repo.trades.GetVM(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var trade = _repo.trades.Get(id);
            _repo.trades.Delete(id);
            return RedirectToAction("Details", "Drafts", new { id = trade.DraftId });
        }

        [HttpPost]
        public PartialViewResult RemoveItem(int id)
        {
            var item = _repo.trades.RemoveItem(id);
            return UpdateTeamView(item.TradeId, item.FromTeamId);
        }

        [HttpPost]
        public PartialViewResult AddItem(int tradeId, int fromTeamId, int round, int? selection, int? playerId)
        {
            var item = new TradeItem
            {
                TradeId = tradeId,
                FromTeamId = fromTeamId,
                Round = round,
                Selection = selection,
                PlayerId = playerId,
                IsPlayer = playerId != null
            };
            _repo.trades.AddItem(item);
            return UpdateTeamView(tradeId, fromTeamId);
        }

        private PartialViewResult UpdateTeamView(int tradeId, int teamId)
        {
            var vm = _repo.trades.GetVM(tradeId);
            return PartialView("_TeamSection", vm.TradeTeam1.Team.Id == teamId ? vm.TradeTeam1 : vm.TradeTeam2);
        }
    }
}