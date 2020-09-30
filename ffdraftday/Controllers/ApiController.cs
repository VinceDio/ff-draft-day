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

        [HttpGet("api/drafts/{draftId}")]

        public JsonResult GetDraft(int draftId)
        {
            var draft = _repo.drafts.Get(draftId);
            var dto = new DTO.Draft
            {
                Id = draft.Id,
                Name = draft.Name,
                Location = draft.Location,
                Commissioner = draft.Commissioner,
                StartTime = draft.StartTime,
                NumberOfTeams = draft.NumberOfTeams,
                Rounds = draft.Rounds,
                ClockSeconds = draft.ClockSeconds,
                Status = draft.Status,
                CurrentPick = draft.CurrentPick
            };
            dto.Teams = draft.Teams.Select(t => new DTO.Team {
                Id = t.Id,
                Name = t.Name,
                Owner = t.Owner,
                DraftPosition = t.DraftPosition
            }).ToList();
            dto.RosterPositions = draft.RosterPositions.Select(r => new DTO.RosterPosition {
                Position = r.Position,
                Sequence = r.Sequence
            }).ToList();
            return Json(dto);
        }

        [HttpGet("api/drafts/{draftId}/picks")]
        public JsonResult GetPicks(int draftId)
        {
            var picks = _repo.drafts.GetPicks(draftId);
            List<DTO.Pick> dto = picks.Select(p => new DTO.Pick
            {
                OverallPick = p.OverallPick,
                Round = p.Round,
                Selection = p.Selection,
                Team = new DTO.Team { Id = p.Team.Id, Name = p.Team.Name, Owner = p.Team.Owner },
                Note = p.Note,
                Player = p.Player == null ? null : new DTO.Player { Id = p.Player.Id, Name = p.Player.Name, Position = p.Player.Position, NFLTeam = p.Player.NFLTeam },
                IsKeeper = p.IsKeeper,
                AutoPick = p.AutoPick
            }).ToList();
            return Json(dto);
        }

        [HttpGet("api/drafts/{draftId}/players")]
        public JsonResult GetPlayers(int draftId)
        {
            var yr = _repo.drafts.Get(draftId).StartTime.Year;
            var players = _repo.players.GetPlayerRanks(yr);
            List<DTO.Player> dto = players.Select(p => new DTO.Player
            {
                Id = p.PlayerId,
                Name = p.Player.Name,
                Position = p.Player.Position,
                NFLTeam = p.Player.NFLTeam,
                Bye = p.Bye,
                Rank = p.Rank
            }).ToList();
            return Json(dto);
        }

  
    }
}