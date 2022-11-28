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
using Microsoft.AspNetCore.Http;

namespace POE.Controllers
{
    public class StudySessionsController : Controller
    {
        private readonly Prog6212P2Context _context;

        public StudySessionsController(Prog6212P2Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            PopulateFindCombobox();
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var sessions = from s in _context.StudySessions
                           where s.UserId == userID
                           select new DGSession
                           {
                               ModuleCode = s.ModuleCode,
                               TimeStudied = TimeSpan.FromTicks(s.HoursStudied),
                               DateStudied = s.DateStudied
                           };
            ViewData["Sessions"] = await sessions.ToListAsync();           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("ModCode")] DGSession session)
        {
            string code = session.ModCode;
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var sessions = from s in _context.StudySessions
                           where s.ModuleCode == code && s.UserId == userID
                           select new DGSession
                        {
                            ModuleCode = s.ModuleCode,
                            TimeStudied = TimeSpan.FromTicks(s.HoursStudied),
                            DateStudied = s.DateStudied
                        };
            ViewData["Sessions"] = await sessions.ToListAsync();

            PopulateFindCombobox();
            return View();
        }       

        // GET: StudySessions/Create
        public IActionResult Create()
        {
            PopulateStudySessionComboBox();
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
                return RedirectToAction("Index","Modules");
            }
            PopulateStudySessionComboBox();
            return View(studySession);
        }

        public void PopulateFindCombobox()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = (from m in _context.Modules
                          join s in _context.StudySessions on m.ModuleCode equals s.ModuleCode
                          where s.UserId == userID
                           select m).Distinct();
            ViewData["Modules"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
        }

        public void PopulateStudySessionComboBox()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m;
            ViewData["AddStudySession"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
        }
    }
}
