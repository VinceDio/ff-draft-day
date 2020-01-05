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
    }
}
