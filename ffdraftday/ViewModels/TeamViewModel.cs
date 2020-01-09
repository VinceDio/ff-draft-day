using ffdraftday.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ffdraftday.ViewModels
{
    public class TeamViewModel
    {
        public Team Team;
        public List<Pick> Picks;
        public List<Trade> Trades;
        public List<Keeper> Keepers;
    }
}
