using DbManagement;
using DbManagement.Models;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modules;
using BindAttribute = Microsoft.AspNetCore.Mvc.BindAttribute;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;

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
            if (sessions == null || sessions.Count() == 0)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                PopulateFindCombobox();
                return View();
            }
            
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
            if (sessions == null || sessions.Count() == 0)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                PopulateFindCombobox();
                return View();
            }
        }

        // GET: StudySessions/Create
        public IActionResult Create()
        {
            ModuleManagement module = new ModuleManagement();
            List<ModuleEntry> mods = module.GetModules(HttpContext.Session.GetInt32("UserID").Value);
            if (mods.Count() == 0 || mods == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                PopulateStudySessionComboBox();
                return View();
            }
            
        }

        // POST: StudySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleCode,HoursStudied,DateStudied")] StudySessionHtmlModel studySession)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            if (ModelState.IsValid)
            {
                StudySessionManagement moduleManagement = new StudySessionManagement();
                await moduleManagement.ConvertStudySession(studySession, HttpContext.Session.GetInt32("UserID").Value);
                return RedirectToAction("Index", "Modules");
            }
            PopulateStudySessionComboBox();
            return View(studySession);
        }

        public void PopulateFindCombobox()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = (from m in _context.Modules
                                     join s in _context.StudySessions on m.ModuleCode equals s.ModuleCode
                                     where s.UserId == userID
                                     select m).Distinct();
            ViewData["Modules"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
        }

        public void PopulateStudySessionComboBox()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m;
            ViewData["AddStudySession"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
        }


        public async Task<IActionResult> SessionsRequired(string sortOrder)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            

            var modules = (from m in _context.Modules
                          join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                          where me.UserId == userID
                          orderby m.ModuleCode
                          select new DGModule
                          {
                              ModuleCode = m.ModuleCode,
                              SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours),
                              HoursStudied = TimeSpan.FromTicks(me.HoursStudied),
                              HoursLeft = TimeSpan.FromTicks(me.HoursLeft)
                          }).AsEnumerable();
            
            if (modules == null || modules.Count() == 0)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.SelfHoursParm = sortOrder == "Self" ? "self_desc" : "Self";
                ViewBag.StudiedParm = sortOrder == "Studied" ? "studied_desc" : "Studied";
                ViewBag.LeftParm = sortOrder == "Left" ? "left_desc" : "Left";
                switch (sortOrder)
                {
                    case "name_desc":
                        modules = modules.OrderByDescending(s => s.ModuleCode);
                        break;
                    case "Self":
                        modules = modules.OrderBy(s => s.SelfStudyHours);
                        break;
                    case "self_desc":
                        modules = modules.OrderByDescending(s => s.SelfStudyHours);
                        break;
                    case "Studied":
                        modules = modules.OrderBy(s => s.HoursStudied);
                        break;
                    case "studied_desc":
                        modules = modules.OrderByDescending(s => s.HoursStudied);
                        break;
                    case "Left":
                        modules = modules.OrderBy(s => s.HoursLeft);
                        break;
                    case "left_desc":
                        modules = modules.OrderByDescending(s => s.HoursLeft);
                        break;
                    default:  // Name ascending 
                        modules = modules.OrderBy(s => s.ModuleCode);
                        break;
                }
                ViewData["HoursLeft"] = modules;
                return View();
            }


        }
    }
}
