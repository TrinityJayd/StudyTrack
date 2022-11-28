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
            if (userID == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            else if (userSemester == null)
            {
                return RedirectToAction("Create", "UserSemesters");
            }
            else
            {
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
            if (mod.CheckLimit(userID))
            {
                return RedirectToAction("ModuleLimit", "Modules");
            }
            else
            {
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
                if (mod.CheckLimit(userID))
                {
                    return RedirectToAction("ModuleLimit", "Modules");
                }
                else
                {
                    //check if the module already exists
                    if (mod.ModuleExistsInDB(module) == true)
                    {

                        if (mod.ModuleExistsInModuleEntry(module, userID) == true)
                        {
                            ModelState.AddModelError("ModuleCode", "Module already exists in your module list");
                        }
                        else
                        {
                            var matchingMod = mod.GetMatchingModule(module);
                            await mod.CreateModuleEntry(matchingMod, userID);
                            await PopulateListBox();
                            //Clear the form
                            ModelState.Clear();
                        }
                    }
                    else
                    {
                        await mod.AddModule(module, userID);
                        await mod.CreateModuleEntry(module, userID);
                   
                        //Clear the form
                        ModelState.Clear();
                        
                    }
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

                await mod.DeleteModule(@module, userID);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult NoModules()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            return View();
        }

        public IActionResult ModuleLimit()
        {
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            return View();
        }

        public async Task<IActionResult> PopulateListBox()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = HttpContext.Session.GetInt32("UserID").Value;
            var prog6212P2Context = await (from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m.ModuleCode).ToListAsync();
            if(prog6212P2Context == null || prog6212P2Context.Count == 0)
            {
                return RedirectToAction("NoModules", "Modules");
                
            }
            else
            {
                ViewData["lstModules"] = prog6212P2Context;
                return View();
            }
            
        }


    }
}
