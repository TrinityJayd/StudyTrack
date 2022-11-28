using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbManagement.Models;

namespace POE.Views
{
    public class ModuleEntriesController : Controller
    {
        private readonly Prog6212P2Context _context;

        public ModuleEntriesController(Prog6212P2Context context)
        {
            _context = context;
        }

        // GET: ModuleEntries
        public async Task<IActionResult> Index()
        {
            var prog6212P2Context = _context.ModuleEntries.Include(m => m.Module).Include(m => m.User);
            return View(await prog6212P2Context.ToListAsync());
        }

        // GET: ModuleEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ModuleEntries == null)
            {
                return NotFound();
            }

            var moduleEntry = await _context.ModuleEntries
                .Include(m => m.Module)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (moduleEntry == null)
            {
                return NotFound();
            }

            return View(moduleEntry);
        }

        // GET: ModuleEntries/Create
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber");
            return View();
        }

        // POST: ModuleEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryId,HoursStudied,HoursLeft,ModuleId,UserId")] ModuleEntry moduleEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moduleEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode", moduleEntry.ModuleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber", moduleEntry.UserId);
            return View(moduleEntry);
        }

        // GET: ModuleEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ModuleEntries == null)
            {
                return NotFound();
            }

            var moduleEntry = await _context.ModuleEntries.FindAsync(id);
            if (moduleEntry == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode", moduleEntry.ModuleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber", moduleEntry.UserId);
            return View(moduleEntry);
        }

        // POST: ModuleEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryId,HoursStudied,HoursLeft,ModuleId,UserId")] ModuleEntry moduleEntry)
        {
            if (id != moduleEntry.EntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduleEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleEntryExists(moduleEntry.EntryId))
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
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode", moduleEntry.ModuleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber", moduleEntry.UserId);
            return View(moduleEntry);
        }

        // GET: ModuleEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ModuleEntries == null)
            {
                return NotFound();
            }

            var moduleEntry = await _context.ModuleEntries
                .Include(m => m.Module)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (moduleEntry == null)
            {
                return NotFound();
            }

            return View(moduleEntry);
        }

        // POST: ModuleEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ModuleEntries == null)
            {
                return Problem("Entity set 'Prog6212P2Context.ModuleEntries'  is null.");
            }
            var moduleEntry = await _context.ModuleEntries.FindAsync(id);
            if (moduleEntry != null)
            {
                _context.ModuleEntries.Remove(moduleEntry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleEntryExists(int id)
        {
          return _context.ModuleEntries.Any(e => e.EntryId == id);
        }
    }
}
