using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ffdraftday.Models;

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
            var team = _db.Team.Where(t => t.Id == id)
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
    }
}
