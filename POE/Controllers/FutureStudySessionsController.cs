using DbManagement;
using DbManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Modules;

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
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;

            var sessions = from s in _context.FutureStudySessions
                           join m in _context.Modules on s.ModuleId equals m.ModuleId
                           join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                           where s.UserId == userID && me.UserId == userID
                           select new FutureStudySessionHtmlModel
                           {
                               ModuleCode = m.ModuleCode,
                               DateToStudy = s.DateToStudy
                           };

            ViewData["Sessions"] = await sessions.ToListAsync();
            if (sessions == null || sessions.Count() == 0)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                PopulateStudySessionComboBox();
                return View();
            }
        }

        // GET: FutureStudySessions/Create
        public IActionResult Create()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            ModuleManagement mod = new ModuleManagement();
            var modules = mod.GetModules(userID);
            if (modules.Count() == 0 || modules == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                PopulateStudySessionComboBox();
                return View();
            }

        }

        // POST: FutureStudySessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleCode,DateToStudy")] FutureStudySessionHtmlModel futureStudySession)
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            if (ModelState.IsValid)
            {
                FutureStudySessionManagement futureStudySessionManagement = new FutureStudySessionManagement();
                if (futureStudySession.DateToStudy <= DateTime.Now)
                {
                    ModelState.AddModelError("DateToStudy", "Date must be in the future.");
                }
                else if (futureStudySessionManagement.CheckIfSessionAlreadyScheduled(futureStudySession.DateToStudy, userID))   
                {
                    ModelState.AddModelError("DateToStudy", "You already have a module scheduled for this day.");
                }
                else
                {                  
                    await futureStudySessionManagement.ConvertFutureSession(futureStudySession, userID);
                    return RedirectToAction("Index", "Modules");
                }               
            }
            PopulateStudySessionComboBox();
            return View(futureStudySession);
        }

        public void PopulateStudySessionComboBox()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m;
            ViewData["Modules"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
        }
    }
}
