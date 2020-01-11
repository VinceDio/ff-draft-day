using ffdraftday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.ViewModels
{
    public class TradeViewModel
    {
        public Trade Trade;
        public TradeViewModelTeam TradeTeam1;
        public TradeViewModelTeam TradeTeam2;
    }

    public class TradeViewModelTeam
    {
        public Team Team;
        public List<Pick> Picks;
        public List<TradeItem> Items;
    }
}
