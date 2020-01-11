using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ffdraftday.Models;

namespace ffdraftday.Repos
{
    public class Repo
    {
        private ffdraftdayContext _db;

        public DraftRepo drafts;
        public TeamRepo teams;
        public PlayerRepo players;
        public KeeperRepo keepers;
        public TradeRepo trades;
        public Repo(ffdraftdayContext db)
        {
            _db = db;
            drafts = new DraftRepo(_db);
            teams = new TeamRepo(_db);
            players = new PlayerRepo(_db);
            keepers = new KeeperRepo(_db);
            trades = new TradeRepo(_db);
        }


    }
}
