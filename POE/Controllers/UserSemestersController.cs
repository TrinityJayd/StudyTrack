using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbManagement.Models;
using Modules;

namespace POE.Controllers
{
    public class UserSemestersController : Controller
    {
        private readonly Prog6212P2Context _context;

        public UserSemestersController(Prog6212P2Context context)
        {
            _context = context;
        }

        // GET: UserSemesters
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: UserSemesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserSemesters == null)
            {
                return NotFound();
            }

            int userID = HttpContext.Session.GetInt32("UserID").Value;
            //get semester details where user id matches the semester user id
            var userSemester = await _context.UserSemesters
                .FirstOrDefaultAsync(m => m.UserId == userID);

            
            if (userSemester == null)
            {
                return NotFound();
            }

            return View(userSemester);
        }

        // GET: UserSemesters/Create
        public IActionResult Create()
        {           
            return View();
        }

        // POST: UserSemesters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemesterStartDate,WeeksInSemester")] UserSemester userSemester)
        {
            if (ModelState.IsValid)
            {
                SemesterManagement semester = new SemesterManagement();
                userSemester.UserId = HttpContext.Session.GetInt32("UserID");
                await semester.AddSemester(userSemester);
                return RedirectToAction(nameof(Index));
            }           
            return View();
        }

        

        
    }
}
