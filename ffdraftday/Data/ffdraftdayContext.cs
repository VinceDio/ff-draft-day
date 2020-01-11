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
        public DbSet<Trade> Trade { get; set; }
        public DbSet<TradeItem> TradeItem { get; set; }
        public DbSet<Keeper> Keeper { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Team>().HasOne<Draft>(t => t.Draft).WithMany(d => d.Teams).HasForeignKey(t => t.DraftId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Pick>().HasOne<Draft>(p => p.Draft).WithMany(d => d.Picks).HasForeignKey(p => p.DraftId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Pick>().HasOne<Team>(p => p.Team).WithMany(t => t.Picks).HasForeignKey(p => p.TeamId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Pick>().HasOne<Player>(p => p.Player).WithMany(pl => pl.Picks).HasForeignKey(p => p.PlayerId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TradeItem>().HasOne<Trade>(a => a.Trade).WithMany(b => b.Items).HasForeignKey(a => a.TradeId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TradeItem>().HasOne<Player>(a => a.Player).WithMany(b => b.TradeItems).HasForeignKey(a => a.PlayerId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<TradeItem>().HasOne<Team>(a => a.FromTeam).WithMany(b => b.TradeItems).HasForeignKey(a => a.FromTeamId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Trade>().HasOne<Draft>(a => a.Draft).WithMany(b => b.Trades).HasForeignKey(a => a.DraftId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Trade>().HasOne<Team>(a => a.Team1).WithMany(b => b.Trades1).HasForeignKey(a => a.Team1Id).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Trade>().HasOne<Team>(a => a.Team2).WithMany(b => b.Trades2).HasForeignKey(a => a.Team2Id).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Keeper>().HasOne<Player>(a => a.Player).WithMany(b => b.Keepers).HasForeignKey(a => a.PlayerId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Keeper>().HasOne<Team>(a => a.Team).WithMany(b => b.Keepers).HasForeignKey(a => a.TeamId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PlayerRank>().HasOne<Player>(r => r.Player).WithOne(p => p.PlayerRank).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RosterPosition>().HasOne<Draft>(p => p.Draft).WithMany(d => d.RosterPositions).HasForeignKey(p => p.DraftId).OnDelete(DeleteBehavior.Restrict);
        }

    }
}
