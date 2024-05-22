using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class AvtorKnigasController : Controller
    {
        private readonly StoreContext _context;

        public AvtorKnigasController(StoreContext context)
        {
            _context = context;
        }

        // GET: AvtorKnigas
        public async Task<IActionResult> Index()
        {
            var storeContext = _context.AvtorKniga.Include(a => a.Avtor).Include(a => a.Kniga);
            return View(await storeContext.ToListAsync());
        }

        // GET: AvtorKnigas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtorKniga = await _context.AvtorKniga
                .Include(a => a.Avtor)
                .Include(a => a.Kniga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avtorKniga == null)
            {
                return NotFound();
            }

            return View(avtorKniga);
        }

        // GET: AvtorKnigas/Create
        public IActionResult Create()
        {
            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "Ime");
            ViewData["KnigaId"] = new SelectList(_context.Kniga, "Id", "Naslov");
            return View();
        }

        // POST: AvtorKnigas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KnigaId,AvtorId")] AvtorKniga avtorKniga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avtorKniga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "FullName", avtorKniga.AvtorId);
            ViewData["KnigaId"] = new SelectList(_context.Kniga, "Id", "FullName", avtorKniga.KnigaId);
            return View(avtorKniga);
        }

        // GET: AvtorKnigas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtorKniga = await _context.AvtorKniga.FindAsync(id);
            if (avtorKniga == null)
            {
                return NotFound();
            }
            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "Ime", avtorKniga.AvtorId);
            ViewData["KnigaId"] = new SelectList(_context.Kniga, "Id", "Naslov", avtorKniga.KnigaId);
            return View(avtorKniga);
        }

        // POST: AvtorKnigas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KnigaId,AvtorId")] AvtorKniga avtorKniga)
        {
            if (id != avtorKniga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avtorKniga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvtorKnigaExists(avtorKniga.Id))
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
            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "Ime", avtorKniga.AvtorId);
            ViewData["KnigaId"] = new SelectList(_context.Kniga, "Id", "Naslov", avtorKniga.KnigaId);
            return View(avtorKniga);
        }

        // GET: AvtorKnigas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avtorKniga = await _context.AvtorKniga
                .Include(a => a.Avtor)
                .Include(a => a.Kniga)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avtorKniga == null)
            {
                return NotFound();
            }

            return View(avtorKniga);
        }

        // POST: AvtorKnigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avtorKniga = await _context.AvtorKniga.FindAsync(id);
            if (avtorKniga != null)
            {
                _context.AvtorKniga.Remove(avtorKniga);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvtorKnigaExists(int id)
        {
            return _context.AvtorKniga.Any(e => e.Id == id);
        }
    }
}
