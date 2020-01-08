using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.Models
{
    public class Trade
    {
        public int Id { get; set; }
        public int DraftId { get; set; }
        public int Team1Id { get; set; }
        public int Team2Id { get; set; }

        public Draft Draft { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        public List<TradeItem> Items { get; set; }
    }
}
