using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class TradeItem
    {
        public int Id { get; set; }
        public int TradeId { get; set; }
        public int FromTeamId { get; set; }
        public bool IsPlayer { get; set; }
        public int Round { get; set; }
        public int? Selection { get; set; }
        public int? PlayerId { get; set; }

        public Trade Trade { get; set; }
        public Team FromTeam { get; set; }
        public Player Player { get; set; }
    }
}
