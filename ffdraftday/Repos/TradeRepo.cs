using ffdraftday.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ffdraftday.ViewModels;
using System.Threading.Tasks;

namespace ffdraftday.Repos
{
    public class TradeRepo
    {
        private ffdraftdayContext _db;

        public TradeRepo(ffdraftdayContext db)
        {
            _db = db;
        }
        internal Trade Get(int id)
        {
            var trade =_db.Trade
                .Include(t => t.Draft)
                .Include(t => t.Team1).ThenInclude(t => t.Picks)
                .Include(t => t.Team2).ThenInclude(t => t.Picks)
                .Include(t => t.Items).ThenInclude(i => i.Player)
                .Where(t => t.Id == id)
                .FirstOrDefault();
            return trade;
        }

        public List<Trade> List(int draftId)
        {
            var trades = _db.Trade.Where(t => t.DraftId == draftId)
                .Include(t => t.Team1)
                .Include(t => t.Team2)
                .Include(t => t.Items).ThenInclude(i => i.Player)
                .OrderBy(t => t.Team1.Name);
            return trades.ToList();
        }

        public void Add(Trade trade)
        {
            _db.Trade.Add(trade);
            _db.SaveChanges();
        }

        public TradeViewModel GetVM(int id)
        {
            var trade = Get(id);
            if (trade == null) return null;
            var vm = new TradeViewModel();
            vm.Trade = trade;

            vm.TradeTeam1 = new TradeViewModelTeam { Team = trade.Team1 };
            vm.TradeTeam1.Items = trade.Items.Where(i => i.FromTeamId == trade.Team1Id).ToList();
            vm.TradeTeam1.Picks = trade.Team1.Picks;

            vm.TradeTeam2 = new TradeViewModelTeam { Team = trade.Team2 };
            vm.TradeTeam2.Items = trade.Items.Where(i => i.FromTeamId == trade.Team2Id).ToList();
            vm.TradeTeam2.Picks = trade.Team2.Picks;

            vm = UndoTradedPicks(vm);
            return vm;
        }

        private TradeViewModel UndoTradedPicks(TradeViewModel vm)
        {
            foreach(TradeItem item in vm.Trade.Items.Where(i => !i.IsPlayer))
            {
                if (!item.IsPlayer)
                {
                    if (item.FromTeamId == vm.Trade.Team1Id)
                    {
                        var pick = vm.TradeTeam2.Picks.FirstOrDefault(p => p.Round == item.Round && p.Selection == item.Selection);
                        if (pick != null)
                        {
                            pick.Note = "";
                            vm.TradeTeam1.Picks.Add(pick);
                            vm.TradeTeam2.Picks.Remove(pick);
                        }
                    }
                    else
                    {
                        var pick = vm.TradeTeam1.Picks.FirstOrDefault(p => p.Round == item.Round && p.Selection == item.Selection);
                        if (pick != null)
                        {
                            pick.Note = "";
                            vm.TradeTeam2.Picks.Add(pick);
                            vm.TradeTeam1.Picks.Remove(pick);
                        }
                    }
                }
            }
            return vm;
        }
    }
}
