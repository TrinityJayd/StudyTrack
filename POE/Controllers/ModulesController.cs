using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbManagement.Models;
using Modules;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using ValidateAntiForgeryTokenAttribute = Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;
using DbManagement;
using BindAttribute = Microsoft.AspNetCore.Mvc.BindAttribute;

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
            if (userID == 0 )
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
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
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
        public async Task<IActionResult> Create([Bind("ModuleId,ModuleCode,ModuleName,Credits,ClassHours")] Module @module)
        {
            
            if (ModelState.IsValid)
            {
                ModuleManagement mod = new ModuleManagement();
                
                int userID = HttpContext.Session.GetInt32("UserID").Value;

                bool existsForUser = mod.ModuleExistsInModuleEntry(@module, userID);
                if (existsForUser)
                {
                    ModelState.AddModelError("ModuleCode", "You have already added this module.");
                    return View();
                }
                else
                {
                    await mod.AddModule(@module, userID);
                    
                   // return RedirectToAction("Index", "Modules");
                }
                
            }
            return View();
        }

        

        // GET: Modules/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
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
                return Problem("Entity set 'Prog6212P2Context.Modules'  is null.");
            }
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                _context.Modules.Remove(@module);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
