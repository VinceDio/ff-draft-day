using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ffdraftday.Models;

namespace ffdraftday.Controllers
{
    public class DraftsController : Controller
    {
        private readonly ffdraftdayContext _context;
        private Repos.Repo _repo;

        public DraftsController(ffdraftdayContext context, Repos.Repo repo)
        {
            _context = context;
            _repo = repo;
        }

        // GET: Drafts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Draft.ToListAsync());
        }

        // GET: Drafts/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var draft = _repo.drafts.Get(id);
            if (draft == null)
            {
                return NotFound();
            }

            return View(draft);
        }

        // GET: Drafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drafts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Commissioner,Location,NumberOfTeams,Rounds,ClockSeconds,StartTime")] Draft draft)
        {
            if (ModelState.IsValid)
            {
                _repo.drafts.Add(draft);
                for(int t = 0; t < draft.NumberOfTeams; t++ )
                {
                    _repo.teams.AddDefault(draft.Id);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(draft);
        }

        // GET: Drafts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Draft.FindAsync(id);
            if (draft == null)
            {
                return NotFound();
            }
            return View(draft);
        }

        // POST: Drafts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Commissioner,Location,NumberOfTeams,Rounds,ClockSeconds,StartTime")] Draft draft)
        {
            if (id != draft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(draft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DraftExists(draft.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(draft);
        }

        // GET: Drafts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var draft = await _context.Draft
                .FirstOrDefaultAsync(m => m.Id == id);
            if (draft == null)
            {
                return NotFound();
            }

            return View(draft);
        }

        // POST: Drafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _repo.drafts.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DraftExists(int id)
        {
            return _context.Draft.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Positions(int draftId)
        {
            var draft = _repo.drafts.Get(draftId);
            if (draft == null) return NotFound("Draft id not found");
            ViewBag.Draft = draft;
            var draftPositions = new List<RosterPosition>();
            if (draft.RosterPositions != null) draftPositions = draft.RosterPositions.OrderBy(p => p.Sequence).ToList();
            ViewBag.Positions = _repo.players.PositionList();
            return View(draftPositions);
        }

        [HttpPost]
        public async Task<IActionResult> AddPosition(int draftId, string position)
        {
            _repo.drafts.AddPosition(draftId, position);
            await _context.SaveChangesAsync();
            return RedirectToAction("Positions", new { draftId });
        }


        [HttpPost]
        public async Task<IActionResult> RemovePosition(int id)
        {
            var pos = _context.RosterPosition.Find(id);
            _context.RosterPosition.Remove(pos);
            await _context.SaveChangesAsync();
            return RedirectToAction("Positions", new { draftId = pos.DraftId });
        }
    }
}
