using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Designa.Data;
using Designa.Models;

namespace Designa.Controllers
{
    public class IrmaosController : Controller
    {
        private readonly DesignaContext _context;

        public IrmaosController(DesignaContext context)
        {
            _context = context;
        }

        // GET: Irmaos
        public async Task<IActionResult> Index()
        {
              return _context.Irmaos != null ? 
                          View(await _context.Irmaos.ToListAsync()) :
                          Problem("Entity set 'DesignaContext.Irmaos'  is null.");
        }

        // GET: Irmaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Irmaos == null)
            {
                return NotFound();
            }

            var irmao = await _context.Irmaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (irmao == null)
            {
                return NotFound();
            }

            return View(irmao);
        }

        // GET: Irmaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Irmaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Observacao")] Irmao irmao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(irmao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(irmao);
        }

        // GET: Irmaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Irmaos == null)
            {
                return NotFound();
            }

            var irmao = await _context.Irmaos.FindAsync(id);
            if (irmao == null)
            {
                return NotFound();
            }
            return View(irmao);
        }

        // POST: Irmaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Observacao")] Irmao irmao)
        {
            if (id != irmao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(irmao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IrmaoExists(irmao.Id))
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
            return View(irmao);
        }

        // GET: Irmaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Irmaos == null)
            {
                return NotFound();
            }

            var irmao = await _context.Irmaos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (irmao == null)
            {
                return NotFound();
            }

            return View(irmao);
        }

        // POST: Irmaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Irmaos == null)
            {
                return Problem("Entity set 'DesignaContext.Irmaos'  is null.");
            }
            var irmao = await _context.Irmaos.FindAsync(id);
            if (irmao != null)
            {
                _context.Irmaos.Remove(irmao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IrmaoExists(int id)
        {
          return (_context.Irmaos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
