using ffdraftday.Models;
using ffdraftday.Repos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DraftTests
    {


        private Repo _repo;
        private ffdraftdayContext _db;
        private Draft _draft;
        private Team _team;

        [SetUp]
        public void Setup()
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


        [Test, Order(1)]
        public void AddDraftWorks()
        {
            _repo.drafts.Add(_draft);
            var draft = _db.Draft.Find(1);
            Assert.True(draft.Name == _draft.Name);
        }

        [Test, Order(2)]
        public void GetDraftWorks()
        {
            var draft = _repo.drafts.Get(_draft.Id);
            Assert.True(draft.Name == _draft.Name);
        }

        [Test, Order(3)]
        public void GetTeamWorks()
        {
            _team = _repo.teams.Get(1);
            Assert.True(_team.DraftId == _draft.Id);
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
            Assert.True(picks.Count > 0);
        }

        [Test, Order(9)]
        public void CanValidateValidDraft()
        {
            var errors = _repo.drafts.ValidateDraft(_draft.Id);
            Assert.True(errors.Count == 0);
        }

    }
}