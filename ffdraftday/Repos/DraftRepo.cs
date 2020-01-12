using ffdraftday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ffdraftday.Repos
{
    public class DraftRepo
    {
        private ffdraftdayContext _db;
        public DraftRepo(ffdraftdayContext db)
        {
            _db = db;
        }

        public Draft Get(int id)
        {
            var draft = _db.Draft.Where(d => d.Id == id)
                .Include(d => d.RosterPositions)
                .Include(d => d.Teams)
                .Include(d => d.Picks)
                .FirstOrDefault();
            return draft;
        }

        public void Add(Draft draft)
        {
            _db.Draft.Add(draft);
            _db.SaveChanges();
            AddListOfPositions(draft.Id, new List<string> { "QB", "WR", "WR", "RB", "RB", "TE", "W/R/T", "K", "DEF", "BN", "BN", "BN", "BN", "BN", "BN", "IR" });
        }

        private void AddListOfPositions(int draftId, List<string> positions)
        {
            foreach(string pos in positions)
            {
                AddPosition(draftId, pos);
            }
        }

        public void AddPosition(int draftId, string position)
        {
            var seq = _db.RosterPosition.Count(p => p.DraftId == draftId) + 1;
            var pos = new RosterPosition { DraftId = draftId, Position = position, Sequence = seq };
            _db.RosterPosition.Add(pos);
            _db.SaveChanges();
            UpdateTotalRounds(draftId);
            SortPositions(draftId);
        }

        public void SortPositions(int draftId)
        {
            var positions = _db.RosterPosition.Where(r => r.DraftId == draftId);
            var playerRepo = new PlayerRepo(_db);
            foreach(RosterPosition pos in positions)
            {
                pos.Sequence = playerRepo.GetPositionSequence(pos.Position);
                _db.RosterPosition.Update(pos);
            }
            _db.SaveChanges();
        }

        private void UpdateTotalRounds(int draftId)
        {
            var rounds = _db.RosterPosition.Count(p => p.DraftId == draftId);
            var draft = _db.Draft.Find(draftId);
            draft.Rounds = rounds;
            _db.SaveChanges();
        }

        public void Delete(int draftId)
        {
            var picks = _db.Pick.Where(p => p.DraftId == draftId);
            _db.Pick.RemoveRange(picks);

            var teams = _db.Team.Where(t => t.DraftId == draftId);
            _db.Team.RemoveRange(teams);

            var positions = _db.RosterPosition.Where(p => p.DraftId == draftId);
            _db.RosterPosition.RemoveRange(positions);

            var draft = _db.Draft.Find(draftId);
            _db.Draft.Remove(draft);

            _db.SaveChanges();
        }

        public void InitPicks(int draftId)
        {
            var draft = _db.Draft.Find(draftId);
            if (draft == null) throw new Exception("Invalid draft id");
            ChangeDraftStatus(draftId, "Setup");
            var existingPicks = _db.Pick.Where(p => p.DraftId == draftId);
            if (existingPicks != null)
            {
                _db.Pick.RemoveRange(existingPicks);
                _db.SaveChanges();
            }
            var picks = LoadPicks(draft);
            try
            {
                picks = MoveTradedPicks(picks, draft);
                picks = LoadKeepers(picks, draft);
                _db.Pick.AddRange(picks);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _db.Pick.AddRange(picks);
                _db.SaveChanges();
                throw ex;
            }
        }

        private List<Pick> LoadPicks(Draft draft)
        {
            var picks = new List<Pick>();
            int round = 0;
            int overallPick = 0;
            var teams = _db.Team.Where(t => t.DraftId == draft.Id).OrderBy(t => t.DraftPosition).ToList();
            while (round < draft.Rounds)
            {
                round += 1;
                int selection = 0;
                foreach (Team team in teams)
                {
                    overallPick += 1;
                    selection += 1;
                    var pick = new Pick
                    {
                        DraftId = draft.Id,
                        TeamId = team.Id,
                        Round = round,
                        Selection = selection,
                        OverallPick = overallPick
                    };
                    picks.Add(pick);
                }
                //reverse order to snake
                teams.Reverse();
            }
            return picks;
        }

        private List<Pick> MoveTradedPicks(List<Pick> picks, Draft draft)
        {
            var trades = _db.Trade.Where(t => t.DraftId == draft.Id)
                .Include(t => t.Team1)
                .Include(t => t.Team2)
                .Include(t => t.Items);
            foreach (Trade trade in trades)
            {
                foreach (TradeItem item in trade.Items.Where(i => !i.IsPlayer))
                {
                    var toTeam = (item.FromTeamId == trade.Team1.Id ? trade.Team2 : trade.Team1);
                    var pick = picks.Where(p => p.Round == item.Round && p.Selection == item.Selection && p.TeamId == item.FromTeamId).FirstOrDefault();
                    if (pick == null) throw new Exception($"Could not move traded pick: {item.Round}.{item.Selection} from {item.FromTeam.Name}");
                    pick.TeamId = toTeam.Id;
                    pick.Note = "From " + item.FromTeam.Name;
                }
            }
            return picks;
        }

        private List<Pick> LoadKeepers(List<Pick> picks, Draft draft)
        {
            var teams = _db.Team.Where(t => t.DraftId == draft.Id).Include(t => t.Keepers);
            foreach(Team team in teams)
            {
                foreach(Keeper keeper in team.Keepers.OrderBy(k => k.Round))
                {
                    //find last pick that is available to use for keeper that is within the same round or earlier 
                    //(if none available in keeper round, will use first available higher round)
                    var pick = picks.Where(p => p.TeamId == team.Id && p.Round <= keeper.Round && p.PlayerId == null).OrderBy(p => p.OverallPick).LastOrDefault();
                    if (pick == null) throw new Exception($"Could not load keeper: {team.Name} Round {keeper.Round}");
                    pick.PlayerId = keeper.PlayerId;
                    pick.IsKeeper = true;
                }
            }
            return picks;
        }

        public void ChangeDraftStatus(int draftId, string status)
        {
            var draft = _db.Draft.Find(draftId);
            if (draft == null) throw new Exception("Invalid draft id");
            draft.Status = status;
            _db.Update(draft);
            _db.SaveChanges();
        }

        public List<string> ValidateDraft(int draftId)
        {
            ChangeDraftStatus(draftId, "Setup");
            var draft = Get(draftId);
            var errors = new List<string>();
            if (draft.NumberOfTeams < 4) errors.Add("Must have at least 4 teams");
            if (draft.Rounds < 4) errors.Add("Must assign at least 4 roster positions.");
            var picksPerTeam = draft.Picks.GroupBy(p => p.TeamId).Select(g => new { g.Key, PickCount = g.Count() }).ToList();

            var picks = _db.Pick.Where(p => p.DraftId == draftId).ToList();
            picksPerTeam = picks.GroupBy(p => p.TeamId).Select(g => new { g.Key, PickCount = g.Count() }).ToList();

            var invalidTeams = picksPerTeam.Where(p => p.PickCount != draft.Rounds).ToList();
            if (invalidTeams.Any())
            {
                string invalidTeamNames = "";
                foreach (var team in invalidTeams)
                {
                    var teamName = _db.Team.Find(team.Key).Name;
                    if (invalidTeamNames != "") invalidTeamNames += ", ";
                    invalidTeamNames += teamName;
                }
                errors.Add("One or more teams does not have correct number of picks due to unbalanced trade: " + invalidTeamNames);
            }
            if (!errors.Any()) ChangeDraftStatus(draftId, "Ready");
            return errors;
        }
    }
}
