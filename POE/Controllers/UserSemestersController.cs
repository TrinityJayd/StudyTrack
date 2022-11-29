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
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            return View();
        }        

        // GET: UserSemesters/Create
        public IActionResult Create()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            return View();
        }

        // POST: UserSemesters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemesterStartDate,WeeksInSemester")] UserSemester userSemester)
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            if (ModelState.IsValid)
            {
                SemesterManagement semester = new SemesterManagement();
                userSemester.UserId = userID;
                if (semester.SemesterExists(userID))
                {
                    await semester.UpdateSemester(userID, userSemester.SemesterStartDate, userSemester.WeeksInSemester);
                }
                else
                {
                    await semester.AddSemester(userSemester);
                }

                //Redirect to home page
                return RedirectToAction("Create", "Modules");
            }           
            return View();
        }

        

        
    }
}
