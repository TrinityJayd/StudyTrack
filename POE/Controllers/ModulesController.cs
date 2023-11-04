using DbManagement;
using DbManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modules;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;
using BindAttribute = Microsoft.AspNetCore.Mvc.BindAttribute;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;

namespace POE.Controllers
{
    public class ModulesController : Controller
    {
        private readonly Prog6212P2Context _context;

        public ModulesController(Prog6212P2Context context)
        {
            _context = context;

        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            //Code Attribution
            //Author:Mikesdotnetting
            //Link:https://www.mikesdotnetting.com/article/192/transferring-data-between-asp-net-web-pages
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID; ;
            //Check if the user has any semesters in the semester table
            var userSemester = await _context.UserSemesters
                .FirstOrDefaultAsync(m => m.UserId == userID);
            //if the user id is 0 it means there is no one logged in, so redirect them to the login page
            if (userID == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            else if (userSemester == null)
            {
                //if the user has not created a semester, redirect them to the semester page
                return RedirectToAction("Create", "UserSemesters");
            }
            else
            {
                //Check if the user has a study session for the day
                FutureStudySessionManagement futureStudySessionManagement = new FutureStudySessionManagement();
                //Alert the user
                ViewData["Reminder"] = futureStudySessionManagement.SessionToday(userID);
                return View();
            }

        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            //get all modules that are related to module entries and the user
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select new ModuleHtmlModule
                                    {
                                        ModuleId = m.ModuleId,
                                        ModuleCode = m.ModuleCode,
                                        ModuleName = m.ModuleName,
                                        Credits = (int)m.Credits,
                                        ClassHours = TimeSpan.FromHours((double)m.ClassHours),
                                        SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours)
                                    };
            ViewData["ModuleData"] = await prog6212P2Context.ToListAsync();

            //if the list is null, redirect them to the no modules page
            if (prog6212P2Context == null || prog6212P2Context.Count() == 0)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                return View();
            }

        }

        // GET: Modules/Create
        public async Task<IActionResult> Create()
        {
            ModuleManagement mod = new ModuleManagement();
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            //check if the user has reached the maximum number of modules
            if (mod.CheckLimit(userID))
            {
                return RedirectToAction("ModuleLimit", "Modules");
            }
            else
            {
                //add the module to the list box
                await PopulateListBox();
                return View();
            }

        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleCode,ModuleName,Credits,ClassHours")] Module @module)
        {
            ModuleManagement mod = new ModuleManagement();
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            int userID = HttpContext.Session.GetInt32("UserID").Value;

            if (ModelState.IsValid)
            {
                //Check if the user has reached the module limit
                if (mod.CheckLimit(userID))
                {
                    return RedirectToAction("ModuleLimit", "Modules");
                }
                else
                {
                    //check if the module already exists
                    if (mod.ModuleExistsInDB(module) == true)
                    {
                        //check if the user has already added that specific module to their list
                        if (mod.ModuleExistsInModuleEntry(module, userID) == true)
                        {
                            ModelState.AddModelError("ModuleCode", "Module already exists in your module list");
                        }
                        else
                        {
                            //get the module from the database
                            var matchingMod = mod.GetMatchingModule(module);
                            //add its information to the user's module
                            await mod.CreateModuleEntry(matchingMod, userID);
                            await PopulateListBox();
                            //Clear the form
                            ModelState.Clear();
                        }
                    }
                    else
                    {
                        //if the module does not exist at all add it to the modules table and add it for the user
                        await mod.AddModule(module, userID);
                        await mod.CreateModuleEntry(module, userID);
                        await PopulateListBox();
                        //Clear the form
                        ModelState.Clear();
                        
                    }
                    //check if the user has rwached the module limit
                    if (mod.CheckLimit(userID))
                    {
                        return RedirectToAction("ModuleLimit", "Modules");
                    }
                }

            }

            return View();
        }

        // GET: Modules/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {

            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            if (id == null || _context.Modules == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }

            //get the details of the module the user wants to delete
            var @module = await (from m in _context.Modules
                                 where m.ModuleId == id
                                 select new ModuleHtmlModule
                                 {
                                     ModuleId = m.ModuleId,
                                     ModuleCode = m.ModuleCode,
                                     ModuleName = m.ModuleName,
                                     Credits = (int)m.Credits,
                                     ClassHours = TimeSpan.FromHours((double)m.ClassHours),
                                     SelfStudyHours = TimeSpan.FromTicks(m.SelfStudyHours)
                                 }).FirstOrDefaultAsync();
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            if (_context.Modules == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                ModuleManagement mod = new ModuleManagement();
                int userID = HttpContext.Session.GetInt32("UserID").Value;
                //delete the module for the user
                await mod.DeleteModule(@module, userID);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult NoModules()
        {
            //set the user id, to display the logout navbar
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            return View();
        }

        public IActionResult ModuleLimit()
        {
            //set the user id, to display the logout navbar
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            return View();
        }

        public async Task<IActionResult> PopulateListBox()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            //populate the list box with the modules that the user has added
            var prog6212P2Context = await (from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m.ModuleCode).ToListAsync();
            if(prog6212P2Context == null)
            {
                //set an empty list so no error is thrown
                ViewData["lstModules"] = new List<string>();
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                //add the list to the view data
                ViewData["lstModules"] = prog6212P2Context;
                return View();
            }
            
        }


    }
}
