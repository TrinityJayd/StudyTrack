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
            //Get the logged in user's id
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            //set the view data of the page so that the logout navbar can be displayed
            ViewData["UserID"] = userID;

            //Get the logged in user's future study sessions
            var sessions = from s in _context.FutureStudySessions
                           join m in _context.Modules on s.ModuleId equals m.ModuleId
                           join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                           where s.UserId == userID && me.UserId == userID
                           select new FutureStudySessionHtmlModel
                           {
                               ModuleCode = m.ModuleCode,
                               DateToStudy = s.DateToStudy
                           };

            //set the view data of the page to the list
            ViewData["Sessions"] = await sessions.ToListAsync();
            
            //if there are no sessions in the list then redirect the user
            //to the no modules page
            if (sessions == null || sessions.Count() == 0)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                //otherwise, populate the combobox for the create page
                PopulateStudySessionComboBox();
                return View();
            }
        }

        // GET: FutureStudySessions/Create
        public IActionResult Create()
        {
            //Get the logged in user's id
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            
            ModuleManagement mod = new ModuleManagement();
            //get all the modules the user added
            var modules = mod.GetModules(userID);
            //if there are no sessions in the list then redirect the user
            //to the no modules page
            if (modules.Count() == 0 || modules == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                //otherwise, populate the combobox for the create page
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
            //Get the logged in user's id
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            if (ModelState.IsValid)
            {
                FutureStudySessionManagement futureStudySessionManagement = new FutureStudySessionManagement();
                //User's can only added a future stuyd session for a date that is the current date or later
                if (futureStudySession.DateToStudy.Date >= DateTime.Now.Date)
                {
                    await futureStudySessionManagement.ConvertFutureSession(futureStudySession, userID);
                    return RedirectToAction("Index", "Modules");                    
                }
                else if (futureStudySessionManagement.CheckIfSessionAlreadyScheduled(futureStudySession.DateToStudy, userID))   
                {
                    //if a session is already scheduled with a module that day, another session cannot be logged
                    ModelState.AddModelError("DateToStudy", "You already have a module scheduled for this day.");
                }
                else
                {
                    //if the user chooses a date in the past, an error message is displayed
                    ModelState.AddModelError("DateToStudy", "Date must be in the future.");
                }               
            }
            //populate the combobox for the create page
            PopulateStudySessionComboBox();
            return View(futureStudySession);
        }

        public void PopulateStudySessionComboBox()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;

            //populate the combobox for the create page
            //users can only add future study sessions for modules they added
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m;
            ViewData["Modules"] = new SelectList(prog6212P2Context, "ModuleCode", "ModuleCode");
        }
    }
}
