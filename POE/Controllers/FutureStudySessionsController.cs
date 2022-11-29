using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbManagement.Models;

namespace POE.Controllers
{
    public class FutureStudySessionsController : Controller
    {
        private readonly Prog6212P2Context _context;

        public FutureStudySessionsController(Prog6212P2Context context)
        {
            _context = context;
        }

        // GET: FutureStudySessions
        public async Task<IActionResult> Index()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = _context.FutureStudySessions.Include(f => f.Module).Include(f => f.User);
            return View(await prog6212P2Context.ToListAsync());
        }

        // GET: FutureStudySessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FutureStudySessions == null)
            {
                return NotFound();
            }

            var futureStudySession = await _context.FutureStudySessions
                .Include(f => f.Module)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FutureId == id);
            if (futureStudySession == null)
            {
                return NotFound();
            }

            return View(futureStudySession);
        }

        // GET: FutureStudySessions/Create
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber");
            return View();
        }

        // POST: FutureStudySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FutureId,UserId,ModuleId,DateToStudy")] FutureStudySession futureStudySession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(futureStudySession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "ModuleId", "ModuleCode", futureStudySession.ModuleId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber", futureStudySession.UserId);
            return View(futureStudySession);
        }


        // GET: FutureStudySessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FutureStudySessions == null)
            {
                return NotFound();
            }

            var futureStudySession = await _context.FutureStudySessions
                .Include(f => f.Module)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.FutureId == id);
            if (futureStudySession == null)
            {
                return NotFound();
            }

            return View(futureStudySession);
        }

        // POST: FutureStudySessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FutureStudySessions == null)
            {
                return Problem("Entity set 'Prog6212P2Context.FutureStudySessions'  is null.");
            }
            var futureStudySession = await _context.FutureStudySessions.FindAsync(id);
            if (futureStudySession != null)
            {
                _context.FutureStudySessions.Remove(futureStudySession);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
