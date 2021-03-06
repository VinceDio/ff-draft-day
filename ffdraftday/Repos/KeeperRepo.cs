﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ffdraftday.Models;
using Microsoft.EntityFrameworkCore;

namespace ffdraftday.Repos
{
    public class KeeperRepo
    {
        private ffdraftdayContext _db;

        public KeeperRepo(ffdraftdayContext db)
        {
            _db = db;
        }

        public Keeper Get(int id)
        {
            var keeper = _db.Keeper
                .Include(k => k.Player)
                .Include(k => k.Team)
                .Where(k => k.Id == id).FirstOrDefault();
            return keeper;
        }

        internal void Save(Keeper keeper)
        {
            if (keeper.Id == 0) _db.Keeper.Add(keeper);
            else _db.Update(keeper);
            _db.SaveChanges();
        }

        internal void Delete(int id)
        {
            var keeper = _db.Keeper.Find(id);
            _db.Remove(keeper);
            _db.SaveChanges();
        }
    }
}
