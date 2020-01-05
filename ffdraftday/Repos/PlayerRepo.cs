using ffdraftday.Models;
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
