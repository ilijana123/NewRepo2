using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using Store.ViewModels;

namespace Store.Controllers
{
    public class KnigasController : Controller
    {
        private readonly StoreContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public KnigasController(StoreContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment =webHostEnvironment;
        }

        // GET: Knigas
        public async Task<IActionResult> Index(string knigaZanr, string searchStringN, string searchStringG, string searchStringI)
        {
            IQueryable<Kniga> knigi = _context.Kniga.AsQueryable();
            IQueryable<string> genreQuery = _context.Kniga.OrderBy(m => m.Zanr).Select(m => m.Zanr).Distinct();

            if (!string.IsNullOrEmpty(searchStringN))
            {
                knigi = knigi.Where(s => s.Naslov.Contains(searchStringN));
            }
            if (!string.IsNullOrEmpty(searchStringG))
            {
                if (int.TryParse(searchStringG, out int year))
                {
                    knigi = knigi.Where(s => s.Godina == year);
                }
            }
            if (!string.IsNullOrEmpty(searchStringI))
            {
                knigi = knigi.Where(s => s.Izdavac.Contains(searchStringI));
            }
            if (!string.IsNullOrEmpty(knigaZanr))
            {
                knigi = knigi.Where(x => x.Zanr == knigaZanr);
            }

            knigi = knigi.Include(m => m.Avtori).ThenInclude(m => m.Avtor);

            var bookGenreVM = new KnigaZanrViewModel
            {
                Zanrovi = new SelectList(await genreQuery.ToListAsync()),
                Knigi = await knigi.ToListAsync()
            };

            return View(bookGenreVM);
        }

        // GET: Knigas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kniga = await _context.Kniga
                .Include(m => m.Avtori)
                .ThenInclude(m => m.Avtor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (kniga == null)
            {
                return NotFound();
            }

            return View(kniga);
        }

        // GET: Knigas/Create
        [HttpGet]
        public IActionResult Create()
        {
            var avtorList = _context.Avtor.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.FullName }).ToList();
            return View(new AvtorKnigaEditViewModel { AvtorList = avtorList });
        }

        // POST: Knigas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AvtorKnigaEditViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                if (bookVM.kniga.SlikaFile != null && bookVM.kniga.SlikaFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(bookVM.kniga.SlikaFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await bookVM.kniga.SlikaFile.CopyToAsync(fileStream);
                    }
                    bookVM.kniga.SlikaUrl = uniqueFileName;
                }
                _context.Add(bookVM.kniga);
                await _context.SaveChangesAsync();
                if (bookVM.SelectedAvtors != null && bookVM.SelectedAvtors.Any())
                {
                    foreach (var avtorId in bookVM.SelectedAvtors)
                    {
                        _context.AvtorKniga.Add(new AvtorKniga { KnigaId = bookVM.kniga.Id, AvtorId = avtorId });
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AvtorId"] = new SelectList(_context.Set<Avtor>(), "Id", "FullName");
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

            var kniga = await _context.Kniga
                .Where(m => m.Id == id)
                .Include(m => m.Avtori)
                .ThenInclude(m => m.Avtor)
                .FirstOrDefaultAsync();

            if (kniga == null)
            {
                return NotFound();
            }

            var avtors = _context.Avtor
                .AsEnumerable()  // Switch to client-side evaluation
                .OrderBy(s => s.FullName);

            AvtorKnigaEditViewModel viewmodel = new AvtorKnigaEditViewModel
            {
                kniga = kniga,
                AvtorList = new MultiSelectList(avtors, "Id", "FullName"),
                SelectedAvtors = kniga.Avtori.Select(sa => sa.AvtorId)
            };

            return View(viewmodel);
        }

        // POST: Knigas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AvtorKnigaEditViewModel viewmodel)
        {
            if (id != viewmodel.kniga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (viewmodel.kniga.SlikaFile != null && viewmodel.kniga.SlikaFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(viewmodel.kniga.SlikaFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await viewmodel.kniga.SlikaFile.CopyToAsync(fileStream);
                        }
                        viewmodel.kniga.SlikaUrl = uniqueFileName;
                    }
                    _context.Update(viewmodel.kniga);
                    await _context.SaveChangesAsync();

                    var newAvtorList = viewmodel.SelectedAvtors;
                    var prevAvtorList = _context.AvtorKniga.Where(s => s.KnigaId == id).Select(s => s.AvtorId);

                    var toBeRemoved = _context.AvtorKniga.Where(s => s.KnigaId == id);
                    if (newAvtorList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newAvtorList.Contains(s.AvtorId));
                        foreach (var avtorId in newAvtorList)
                        {
                            if (!prevAvtorList.Any(s => s == avtorId))
                            {
                                _context.AvtorKniga.Add(new AvtorKniga { AvtorId = avtorId, KnigaId = id });
                            }
                        }
                    }

                    _context.AvtorKniga.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnigaExists(viewmodel.kniga.Id))
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

        // GET: Knigas/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kniga = await _context.Kniga.Include(b=>b.Avtori).ThenInclude(b=>b.Avtor).FirstOrDefaultAsync(m => m.Id == id);
            if (kniga == null)
            {
                return NotFound();
            }

            return View(kniga);
        }

        // POST: Knigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kniga = await _context.Kniga.FindAsync(id);
            if (kniga != null)
            {
                _context.Kniga.Remove(kniga);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool KnigaExists(int id)
        {
            return _context.Kniga.Any(e => e.Id == id);
        }
    }
}
