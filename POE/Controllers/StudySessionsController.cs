using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbManagement.Models;
using DbManagement;
using Modules;

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

        // GET: StudySessions/Create
        public IActionResult Create()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m;
            ViewData["Modules"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
            return View();
        }

        // POST: StudySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleCode,HoursStudied,DateStudied")] StudySessionHtmlModel studySession)
        {
            if (ModelState.IsValid)
            {
                ModuleManagement moduleManagement = new ModuleManagement();
                await moduleManagement.AddStudySession(studySession, HttpContext.Session.GetInt32("UserID").Value);
                return RedirectToAction(nameof(Index));
            }
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m;
            ViewData["Modules"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
            return View(studySession);
        }

        
    }
}
