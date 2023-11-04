using DbManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modules;
using Newtonsoft.Json;
using POE.Models;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace POE.Controllers
{
    public class GraphController : Controller
    {
        private readonly Prog6212P2Context _context;

        public GraphController(Prog6212P2Context context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            //Get the user's id
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;
            //Get a list of all the users modules, the self study hours and the hours they already studied
            var moduleEntries = from m in _context.Modules
                                join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                where me.UserId == userID
                                select new
                                {
                                    m.ModuleCode,
                                    m.SelfStudyHours,
                                    me.HoursStudied
                                };
            //populate the combobox so they can filter which module they want to see
            PopulateCombobox();
            
            var data = moduleEntries.ToList();
            //if there is no data, return the no modules page
            if (data == null || data.Count == 0)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
                //Create a list for all the data points
                List<DataPoint> dataPoints = new List<DataPoint>();

                //add each module to the list
                foreach (var module in data)
                {
                    //convert the long to a decimal
                    decimal selfStudy = (decimal)TimeSpan.FromTicks(module.SelfStudyHours).TotalHours;
                    decimal actHours = (decimal)TimeSpan.FromTicks(module.HoursStudied).TotalHours;
                    dataPoints.Add(new DataPoint("Actual Hours " + module.ModuleCode, actHours));
                    dataPoints.Add(new DataPoint("Ideal Hours " + module.ModuleCode, selfStudy));
                }

                //add the list to the viewbag
                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                return View();
            }
        }

        [HttpPost]
        public IActionResult Index([Bind("ModCode")] DataPoint mod)
        {
            ModuleManagement moduleManagement = new ModuleManagement();
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            ViewData["UserID"] = userID;

            //get the module data for the specific module the user wants
            var moduleEntries = from m in _context.Modules
                                join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                where me.UserId == userID && m.ModuleCode == mod.ModCode
                                select new
                                {
                                    m.ModuleCode,
                                    m.SelfStudyHours,
                                    me.HoursStudied
                                };
            PopulateCombobox();
            var data = moduleEntries.ToList();
            if (data == null)
            {
                return RedirectToAction("NoModules", "Modules");
            }
            else
            {
               
                List<DataPoint> dataPoints = new List<DataPoint>();

                foreach (var module in data)
                {
                    decimal selfStudy = (decimal)TimeSpan.FromTicks(module.SelfStudyHours).TotalHours;
                    decimal actHours = (decimal)TimeSpan.FromTicks(module.HoursStudied).TotalHours;
                    dataPoints.Add(new DataPoint("Actual Hours " + module.ModuleCode, actHours));
                    dataPoints.Add(new DataPoint("Ideal Hours " + module.ModuleCode, selfStudy));
                }


                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
                return View();
            }


        }

        public void PopulateCombobox()
        {
            int userID = HttpContext.Session.GetInt32("UserID").Value;
            //populate the combobox so the user cna filter which module they want to see
            var moduleEntries = from m in _context.Modules
                                join me in _context.ModuleEntries on m.ModuleId equals me.ModuleId
                                where me.UserId == userID
                                select new
                                {
                                    m.ModuleCode,
                                    m.SelfStudyHours,
                                    me.HoursStudied
                                };

            ViewData["Modules"] = new SelectList(moduleEntries, "ModuleCode", "ModuleCode");
        }

    }
}
