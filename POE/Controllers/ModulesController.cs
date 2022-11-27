using DbManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modules;
using System.Linq;
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
        private int userID;

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
            userID = HttpContext.Session.GetInt32("UserID").Value;
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
            //get all modules that are related to module entries and the user
            var prog6212P2Context = from m in _context.Modules
                                    join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                    where me.UserId == userID
                                    select m;


            if (prog6212P2Context == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                return View(await prog6212P2Context.ToListAsync());
            }

        }
        
        // GET: Modules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleCode,ModuleName,Credits,ClassHours")] Module @module)
        {

            if (ModelState.IsValid)
            {
                ModuleManagement mod = new ModuleManagement();

                int userID = HttpContext.Session.GetInt32("UserID").Value;

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
                
                return View();
            }
            return View();
        }

        // GET: Modules/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.ModuleId == id);
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
            if (_context.Modules == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                ModuleManagement mod = new ModuleManagement();
                int userID = HttpContext.Session.GetInt32("UserID").Value;
                await mod.DeleteModule(@module,userID);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult NoModules()
        {
            return View();
        }
    }
}
