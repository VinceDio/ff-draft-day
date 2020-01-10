using ffdraftday.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Repos
{
    public class PlayerRepo
    {
        private List<Position> _positions;
        private ffdraftdayContext _db;

        public PlayerRepo(ffdraftdayContext db)
        {
            LoadPositions();
            _db = db;
        }

        public Player Get(int id)
        {
            return _db.Player.Find(id);
        }

        public List<PlayerRank> Search(string text)
        {
            if (text == null) return new List<PlayerRank>();
            if (text.Length < 2) return new List<PlayerRank>();
            text =  text.ToLower();
            var ranks = _db.PlayerRank.Include(p => p.Player)
                .Where(p => p.Year == DateTime.Today.Year && p.Player.Name.ToLower().Contains(text)).ToList();
            //add in non ranked players
            var players = _db.Player.Where(p => p.Name.ToLower().Contains(text));
            foreach (Player player in players)
            {
                if (!ranks.Any(r => r.PlayerId == player.Id))
                {
                    var rank = new PlayerRank
                    {
                        PlayerId = player.Id,
                        Player = player
                    };
                    ranks.Add(rank);
                }
            }
            return ranks.OrderBy(p => p.Rank).ToList();
        }

        public List<Select2Object> PlayerSelectList(string text)
        {
            var players = Search(text);
            List<Select2Object> list = players.Select(p => new Select2Object(p.PlayerId, p.Player.Name)).ToList();
            return list;
        }

        public List<Position> PositionList()
        {
            return _positions;
        }

        private void LoadPositions()
        {
            _positions = new List<Position>();
            _positions.Add(new Position("QB", 1));
            _positions.Add(new Position("WR", 2));
            _positions.Add(new Position("RB", 3));
            _positions.Add(new Position("TE", 4));
            _positions.Add(new Position("W/R/T", 5));
            _positions.Add(new Position("K", 6));
            _positions.Add(new Position("DEF", 7));
            _positions.Add(new Position("IR", 8));
            _positions.Add(new Position("BN", 9));
        }

        public int GetPositionSequence(string positionCode)
        {
            var pos = _positions.Where(p => p.Code == positionCode).FirstOrDefault();
            return pos == null ? 0 : pos.Sequence;
        }
    }
}
