using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using Store.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Store.Controllers
{
    public class AvtorsController : Controller
    {
        private readonly StoreContext _context;

        public AvtorsController(StoreContext context)
        {
            _context = context;
        }

        // GET: Avtors
        public async Task<IActionResult> Index(string searchStringI, string searchStringP, string searchStringN)
        {

            IQueryable<Avtor> avtori = _context.Avtor.AsQueryable();
    
            if (!string.IsNullOrEmpty(searchStringI))
            {
               avtori=avtori.Where(s => s.Ime.Contains(searchStringI));
            }
            if (!string.IsNullOrEmpty(searchStringP))
            {
                avtori = avtori.Where(s => s.Prezime.Contains(searchStringP));
            }
            if (!string.IsNullOrEmpty(searchStringN))
            {
                avtori = avtori.Where(s => s.Nacionalnost.Contains(searchStringN));
            }
            avtori=avtori.Include(m => m.Knigi).ThenInclude(m => m.Kniga);
            var AvtorVM = new AvtorViewModel
            {
                Avtori = await avtori.ToListAsync(),
            };
            return View(AvtorVM);
        }

        // GET: Avtors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor
                .Include(a => a.Knigi)
                .ThenInclude(m => m.Kniga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }

        // GET: Avtors/Create
        [HttpGet]
        public IActionResult Create()
        {
            var knigaList = _context.Kniga.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Naslov }).ToList();
            return View(new KnigaAvtorEditViewModel { KnigaList = knigaList });
        }

        // POST: Knigas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KnigaAvtorEditViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookVM.avtor);
                await _context.SaveChangesAsync();
                if (bookVM.SelectedKnigas != null && bookVM.SelectedKnigas.Any())
                {
                    foreach (var knigaId in bookVM.SelectedKnigas)
                    {
                        _context.AvtorKniga.Add(new AvtorKniga { AvtorId = bookVM.avtor.Id, KnigaId = knigaId });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["KnigaId"] = new SelectList(_context.Set<Kniga>(), "Id", "Naslov");
            return View(bookVM);
        }

        // GET: Knigas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor
                .Where(m => m.Id == id)
                .Include(m => m.Knigi)
                .ThenInclude(m => m.Kniga)
                .FirstOrDefaultAsync();

            if (avtor == null)
            {
                return NotFound();
            }

            var knigi = _context.Kniga
                .AsEnumerable()  // Switch to client-side evaluation
                .OrderBy(s => s.Naslov);

            KnigaAvtorEditViewModel viewmodel = new KnigaAvtorEditViewModel
            {
                avtor=avtor,
                KnigaList = new MultiSelectList(knigi, "Id", "Naslov"),
                SelectedKnigas = avtor.Knigi.Select(sa => sa.KnigaId)
            };

            return View(viewmodel);
        }

        // POST: Knigas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KnigaAvtorEditViewModel viewmodel)
        {
            if (id != viewmodel.avtor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.avtor);
                    await _context.SaveChangesAsync();

                    var newKnigaList = viewmodel.SelectedKnigas;
                    var prevKnigaList = _context.AvtorKniga.Where(s => s.AvtorId == id).Select(s => s.KnigaId);

                    var toBeRemoved = _context.AvtorKniga.Where(s => s.AvtorId == id);
                    if (newKnigaList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newKnigaList.Contains(s.KnigaId));
                        foreach (var knigaId in newKnigaList)
                        {
                            if (!prevKnigaList.Any(s => s == knigaId))
                            {
                                _context.AvtorKniga.Add(new AvtorKniga { KnigaId = knigaId, AvtorId = id });
                            }
                        }
                    }

                    _context.AvtorKniga.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvtorExists(viewmodel.avtor.Id))
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

            return View(viewmodel);
        }

        // GET: Avtors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor
                .Include(a =>a.Knigi)
                .ThenInclude(a=>a.Kniga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }

        // POST: Avtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avtor = await _context.Avtor.FindAsync(id);
            if (avtor != null)
            {
                _context.Avtor.Remove(avtor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvtorExists(int id)
        {
            return _context.Avtor.Any(e => e.Id == id);
        }
    }
}