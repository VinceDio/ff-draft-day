using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ffdraftday.Models;
using Microsoft.EntityFrameworkCore;

namespace ffdraftday.Repos
{
    public class TeamRepo
    {
        private ffdraftdayContext _db;
        public TeamRepo(ffdraftdayContext db)
        {
            _db = db;
        }

        public Team Get(int id)
        {
            var team = _db.Team
                .Include(t => t.Draft)
                .Where(t => t.Id == id)
                .FirstOrDefault();
            return team;
        }

        public void Add(Team team)
        {
            _db.Team.Add(team);
            _db.SaveChanges();
        }

        public void AddDefault(int draftId)
        {
            var pos = _db.Team.Count(t => t.DraftId == draftId) + 1;
            _db.Team.Add(new Team { DraftId = draftId, DraftPosition = pos, Name = $"Team {pos}" });
            _db.SaveChanges();
        }

        public void Update(Team changes)
        {
            var team = _db.Team.Find(changes.Id);
            if (team == null) return;
            team.Name = changes.Name;
            team.Owner = changes.Owner;
            if (changes.DraftPosition > 0 && changes.DraftPosition <= 20) team.DraftPosition = changes.DraftPosition;
            _db.Update(team);
            _db.SaveChanges();
        }

        public List<Team> List(int draftId)
        {
            var teams = _db.Team.OrderBy(t => t.DraftPosition).ToList();
            return teams;
        }

        internal void Move(int id, int direction)
        {
            var movingTeam  = Get(id);
            if (movingTeam == null) throw new Exception("Team not found");
            var maxPos = _db.Team.Count();
            var oldPos = _db.Team.Count(t => t.DraftPosition <= movingTeam.DraftPosition);
            var newPos = direction == -1 ? (oldPos > 1 ? oldPos - 1 : 1) : (oldPos < maxPos ? oldPos + 1 : maxPos);
            var pos = 0;
            foreach(Team team in _db.Team.OrderBy(t => t.DraftPosition))
            {
                pos += 1;
                team.DraftPosition = pos == oldPos ? newPos : (pos == newPos ? oldPos : pos);
                _db.Update(team);   
            }
            _db.SaveChanges();
        }
    }
}
