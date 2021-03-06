using ffdraftday.Models;
using ffdraftday.Repos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {


        private Repo _repo;
        private ffdraftdayContext _db;
        private Draft _draft;
        private Team _team;
        private Team _team2;
        private Trade _trade;

        [OneTimeSetUp]
        public void InitSetup()
        {
            var options = new DbContextOptionsBuilder<ffdraftdayContext>()
                .UseInMemoryDatabase(databaseName: "ffDraftTestDB")
                .Options;
            _db = new ffdraftdayContext(options);
            _repo = new Repo(_db);
            _draft = new Draft
            {
                Id = 1,
                Commissioner = "Commissioner",
                Name = "Test Draft",
                Rounds = 16,
                NumberOfTeams = 10
            };
        }


        [Test, Category("Draft"), Order(1)]
        public void AddDraftWorks()
        {
            _repo.drafts.Add(_draft);
            var draft = _db.Draft.Find(_draft.Id);
            Assert.That(draft.Name, Is.EqualTo(_draft.Name));
        }

        [Test, Category("Draft"), Order(2)]
        public void GetDraftWorks()
        {
            var draft = _repo.drafts.Get(_draft.Id);
            _team = draft.Teams[0];
            _team2 = draft.Teams[1];
            Assert.That(draft.Name, Is.EqualTo(_draft.Name));
        }

        [Test, Category("Team"), Order(3)]
        public void GetTeamWorks()
        {
            var team = _repo.teams.Get(_team.Id);
            Assert.That(team.DraftId, Is.EqualTo(_draft.Id));
        }

        [Test, Order(4)]
        public void InitPicksWorks()
        {
            Assert.DoesNotThrow(() => _repo.drafts.InitPicks(_draft.Id));
        }

        [Test, Order(5)]
        public void GetPicksWorks()
        {
            var picks = _repo.drafts.GetPicks(_draft.Id);
            Assert.That(picks, Has.Count.GreaterThan(0));
        }

        [Test, Order(9)]
        public void CanValidateValidDraft()
        {
            var errors = _repo.drafts.ValidateDraft(_draft.Id);
            Assert.That(errors, Has.Count.EqualTo(0));
        }

        [Test, Order(10)]
        public void AddTradeWorks()
        {
            _trade = new Trade
            {
                DraftId = _draft.Id,
                Team1Id = _team.Id,
                Team2Id = _team2.Id
            };
            _repo.trades.Add(_trade);
            var trades = _repo.trades.List(_draft.Id);
            Assert.That(trades, Has.Count.EqualTo(1));
        }

        [Test, Order(11)]
        public void AddTradeItemWorks()
        {
            var picks = _repo.drafts.GetPicks(_draft.Id);
            var fromPick = picks.Where(p => p.TeamId == _team.Id).First();
            var item1 = new TradeItem
            {
                TradeId = _trade.Id,
                FromTeamId = fromPick.TeamId,
                Round = fromPick.Round,
                Selection = fromPick.Selection,
            };
            _repo.trades.AddItem(item1);
            var trade = _db.Trade.Find(_trade.Id);
            Assert.That(trade.Items[0].Selection, Is.EqualTo(item1.Selection));
        }
        
        [Test, Order(12)]
        public void UnbalancedPicksResultsInInvalidDraft()
        {
            var errors = _repo.drafts.ValidateDraft(_draft.Id);
            StringAssert.Contains("unbalanced trade", errors[0]);
        }

        [Test, Order(13)]
        public void AddTradeItem2Works()
        {
            var picks = _repo.drafts.GetPicks(_draft.Id);
            var toPick = picks.Where(p => p.TeamId == _team2.Id).Last();
            var item2 = new TradeItem
            {
                TradeId = _trade.Id,
                FromTeamId = toPick.TeamId,
                Round = toPick.Round,
                Selection = toPick.Selection,
            };
            _repo.trades.AddItem(item2);
            var trade = _db.Trade.Find(_trade.Id);
            Assert.That(trade.Items, Has.Count.EqualTo(2));
        }

    }
}