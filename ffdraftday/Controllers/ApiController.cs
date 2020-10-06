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
            Models.Pick currPick = draft.CurrentPick == 0 ? null : _repo.drafts.GetPickByOverallSelection(draft.Id, draft.CurrentPick);
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
                CurrentPick = MapPickDTO(currPick)
            };
            dto.Teams = draft.Teams.Select(t => MapTeamDTO(t)).ToList();
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
            List<DTO.Pick> dto = picks.Select(p => MapPickDTO(p)).ToList();
            return Json(dto);
        }

        private DTO.Pick MapPickDTO(Models.Pick pick)
        {
            var dto = new DTO.Pick {
                OverallPick = pick.OverallPick,
                Round = pick.Round,
                Selection = pick.Selection,
                Team = MapTeamDTO(pick.Team),
                Note = pick.Note,
                Player = pick.Player == null ? null : MapPlayerDTO(pick.Player),
                IsKeeper = pick.IsKeeper,
                AutoPick = pick.AutoPick
            };
            return dto;
        }

        private DTO.Team MapTeamDTO(Models.Team team)
        {
            var dto = new DTO.Team
            {
                Id = team.Id,
                Name = team.Name,
                Owner = team.Owner,
                DraftPosition = team.DraftPosition
            };
            return dto;
        }

        private DTO.Player MapPlayerDTO(Models.Player player)
        {
            var dto = new DTO.Player
            {
                Id = player.Id,
                Name = player.Name,
                Position = player.Position,
                NFLTeam = player.NFLTeam
            };
            return dto;
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

  
        [HttpPost("api/drafts/{draftId}/pick")]
        public ActionResult PostDraftPick(int draftId, [FromBody] DTO.Pick pick)
        {
            if (draftId <= 0) return BadRequest("Draft Id not provided");
            if (pick.OverallPick == 0) return BadRequest("Overall Pick number not provided");
            if (pick.Team?.Id == null) return BadRequest("Team id not provided");
            if (pick.Player?.Id == null) return BadRequest("Player id not provided");

            var selection = new Models.Pick
            {
                DraftId = draftId,
                PlayerId = pick.Player.Id,
                TeamId = pick.Team.Id,
                OverallPick = pick.OverallPick,
                AutoPick = pick.AutoPick
            };
            var result = _repo.drafts.DraftPlayer(selection);
            if (result.Success) return Ok(pick);
            else return BadRequest(result.ErrorMessage);
        }
    }
}