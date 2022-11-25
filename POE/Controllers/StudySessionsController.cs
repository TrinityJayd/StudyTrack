﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbManagement.Models;

namespace POE.Controllers
{
    public class StudySessionsController : Controller
    {
        private readonly Prog6212P2Context _context;

        public StudySessionsController(Prog6212P2Context context)
        {
            _context = context;
        }

        // GET: StudySessions
        public async Task<IActionResult> Index()
        {
            var prog6212P2Context = _context.StudySessions.Include(s => s.User);
            return View(await prog6212P2Context.ToListAsync());
        }

        // GET: StudySessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudySessions == null)
            {
                return NotFound();
            }

            var studySession = await _context.StudySessions
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (studySession == null)
            {
                return NotFound();
            }

            return View(studySession);
        }

        // GET: StudySessions/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber");
            return View();
        }

        // POST: StudySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionId,ModuleCode,HoursStudied,DateStudied,UserId")] StudySession studySession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studySession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber", studySession.UserId);
            return View(studySession);
        }

        // GET: StudySessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudySessions == null)
            {
                return NotFound();
            }

            var studySession = await _context.StudySessions.FindAsync(id);
            if (studySession == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber", studySession.UserId);
            return View(studySession);
        }

        // POST: StudySessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionId,ModuleCode,HoursStudied,DateStudied,UserId")] StudySession studySession)
        {
            if (id != studySession.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studySession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudySessionExists(studySession.SessionId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "CellNumber", studySession.UserId);
            return View(studySession);
        }

        // GET: StudySessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudySessions == null)
            {
                return NotFound();
            }

            var studySession = await _context.StudySessions
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (studySession == null)
            {
                return NotFound();
            }

            return View(studySession);
        }

        // POST: StudySessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudySessions == null)
            {
                return Problem("Entity set 'Prog6212P2Context.StudySessions'  is null.");
            }
            var studySession = await _context.StudySessions.FindAsync(id);
            if (studySession != null)
            {
                _context.StudySessions.Remove(studySession);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudySessionExists(int id)
        {
          return _context.StudySessions.Any(e => e.SessionId == id);
        }
    }
}
