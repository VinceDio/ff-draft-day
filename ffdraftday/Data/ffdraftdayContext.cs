using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ffdraftday.Models
{
    public class ffdraftdayContext : DbContext
    {
        public ffdraftdayContext(DbContextOptions<ffdraftdayContext> options)
            : base(options)
        {
        }

        public DbSet<Draft> Draft { get; set; }
        public DbSet<Team> Team { get; set;}
        public DbSet<RosterPosition> RosterPosition { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<PlayerRank> PlayerRank { get; set; }
        public DbSet<Pick> Pick { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Team>().HasOne<Draft>(t => t.Draft).WithMany(d => d.Teams).HasForeignKey(t => t.DraftId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Pick>().HasOne<Draft>(p => p.Draft).WithMany(d => d.Picks).HasForeignKey(p => p.DraftId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Pick>().HasOne<Team>(p => p.Team).WithMany(t => t.Picks).HasForeignKey(p => p.TeamId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Pick>().HasOne<Player>(p => p.Player).WithMany(pl => pl.Picks).HasForeignKey(p => p.PlayerId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PlayerRank>().HasOne<Player>(r => r.Player).WithOne(p => p.PlayerRank).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RosterPosition>().HasOne<Draft>(p => p.Draft).WithMany(d => d.RosterPositions).HasForeignKey(p => p.DraftId).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
