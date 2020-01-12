﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ffdraftday.Models;

namespace ffdraftday.Migrations
{
    [DbContext(typeof(ffdraftdayContext))]
    partial class ffdraftdayContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ffdraftday.Models.Draft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClockSeconds");

                    b.Property<string>("Commissioner");

                    b.Property<string>("Location");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NumberOfTeams");

                    b.Property<int>("Rounds");

                    b.Property<DateTime>("StartTime");

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Draft");
                });

            modelBuilder.Entity("ffdraftday.Models.Keeper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Note");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Round");

                    b.Property<int>("TeamId");

                    b.Property<int>("YearsRemaining");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Keeper");
                });

            modelBuilder.Entity("ffdraftday.Models.Pick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AutoPick");

                    b.Property<int>("DraftId");

                    b.Property<bool>("IsKeeper");

                    b.Property<string>("Note");

                    b.Property<int>("OverallPick");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Round");

                    b.Property<int>("Selection");

                    b.Property<int>("TeamId");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("Id");

                    b.HasIndex("DraftId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TeamId");

                    b.ToTable("Pick");
                });

            modelBuilder.Entity("ffdraftday.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NFLTeam")
                        .HasMaxLength(3);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("ffdraftday.Models.PlayerRank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Bye");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Rank");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("PlayerRank");
                });

            modelBuilder.Entity("ffdraftday.Models.RosterPosition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DraftId");

                    b.Property<string>("Position")
                        .IsRequired();

                    b.Property<int>("Sequence");

                    b.HasKey("Id");

                    b.HasIndex("DraftId");

                    b.ToTable("RosterPosition");
                });

            modelBuilder.Entity("ffdraftday.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DraftId");

                    b.Property<int>("DraftPosition");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Owner");

                    b.HasKey("Id");

                    b.HasIndex("DraftId");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("ffdraftday.Models.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DraftId");

                    b.Property<int>("Team1Id");

                    b.Property<int>("Team2Id");

                    b.HasKey("Id");

                    b.HasIndex("DraftId");

                    b.HasIndex("Team1Id");

                    b.HasIndex("Team2Id");

                    b.ToTable("Trade");
                });

            modelBuilder.Entity("ffdraftday.Models.TradeItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FromTeamId");

                    b.Property<bool>("IsPlayer");

                    b.Property<int?>("PlayerId");

                    b.Property<int>("Round");

                    b.Property<int?>("Selection");

                    b.Property<int>("TradeId");

                    b.HasKey("Id");

                    b.HasIndex("FromTeamId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TradeId");

                    b.ToTable("TradeItem");
                });

            modelBuilder.Entity("ffdraftday.Models.Keeper", b =>
                {
                    b.HasOne("ffdraftday.Models.Player", "Player")
                        .WithMany("Keepers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ffdraftday.Models.Team", "Team")
                        .WithMany("Keepers")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ffdraftday.Models.Pick", b =>
                {
                    b.HasOne("ffdraftday.Models.Draft", "Draft")
                        .WithMany("Picks")
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ffdraftday.Models.Player", "Player")
                        .WithMany("Picks")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ffdraftday.Models.Team", "Team")
                        .WithMany("Picks")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ffdraftday.Models.PlayerRank", b =>
                {
                    b.HasOne("ffdraftday.Models.Player", "Player")
                        .WithOne("PlayerRank")
                        .HasForeignKey("ffdraftday.Models.PlayerRank", "PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ffdraftday.Models.RosterPosition", b =>
                {
                    b.HasOne("ffdraftday.Models.Draft", "Draft")
                        .WithMany("RosterPositions")
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ffdraftday.Models.Team", b =>
                {
                    b.HasOne("ffdraftday.Models.Draft", "Draft")
                        .WithMany("Teams")
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ffdraftday.Models.Trade", b =>
                {
                    b.HasOne("ffdraftday.Models.Draft", "Draft")
                        .WithMany("Trades")
                        .HasForeignKey("DraftId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ffdraftday.Models.Team", "Team1")
                        .WithMany("Trades1")
                        .HasForeignKey("Team1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ffdraftday.Models.Team", "Team2")
                        .WithMany("Trades2")
                        .HasForeignKey("Team2Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ffdraftday.Models.TradeItem", b =>
                {
                    b.HasOne("ffdraftday.Models.Team", "FromTeam")
                        .WithMany("TradeItems")
                        .HasForeignKey("FromTeamId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ffdraftday.Models.Player", "Player")
                        .WithMany("TradeItems")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ffdraftday.Models.Trade", "Trade")
                        .WithMany("Items")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
