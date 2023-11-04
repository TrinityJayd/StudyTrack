using Microsoft.AspNetCore.Mvc;
using POE.Models;
using System.Diagnostics;

namespace POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            //Check if the session variable exists
            if (HttpContext.Session.GetInt32("UserID") == null || HttpContext.Session.GetInt32("UserID") == 0)
            {
                //If it doesn't, create it
                HttpContext.Session.SetInt32("UserID", 0);
                ViewData["UserID"] = 0;
                return View();
            }
            else
            {
                //get the logged in user's id
                ViewData["UserID"] = HttpContext.Session.GetInt32("UserID");
                return RedirectToAction("Index", "Modules");
            }
            
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}