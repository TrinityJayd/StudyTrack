using Microsoft.EntityFrameworkCore;
using Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class ModuleManagement
    {
         
        public async Task AddModule(Module module)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //Find last entry id value
            var lastEntry = await appDataContext.Modules.OrderByDescending(x => x.EntryId).FirstOrDefaultAsync();

            if (lastEntry == null)
            {
                module.EntryId = 1;
            }
            else
            {
                module.EntryId = lastEntry.EntryId + 1;
            }
            //calculate the self study hours
            module.SelfStudyHours = CalculateSelfStudyHours(module);
            //the hours left to study will be the same as the self study hours
            module.HoursLeft = module.SelfStudyHours;
            //add the module to the database
            appDataContext.Modules.Add(module);
            await appDataContext.SaveChangesAsync();
        }

        public async Task DeleteModule(string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //Remove the module from the modules table
            appDataContext.Remove(appDataContext.Modules.Single(m => m.ModuleCode == moduleCode && m.UserId == userID));  
            //remove the module from the study session table
            appDataContext.Remove(appDataContext.StudySessions.Single(m => m.ModuleCode == moduleCode && m.UserId == userID));  
            await appDataContext.SaveChangesAsync();
        }

        public async Task UpdateModule(long hoursStudied, DateTime dateLastStudied, string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //Retrieve the module from the database
            var moduleToUpdate = appDataContext.Modules.Single(m => m.ModuleCode == moduleCode && m.UserId == userID);
            //accumulate the hours studied
            moduleToUpdate.HoursStudied += hoursStudied;

            //create a new study session
            StudySession session = new StudySession
            {
                UserId = userID,
                ModuleCode = moduleCode,
                HoursStudied = hoursStudied,
                DateStudied = dateLastStudied
            };

            StudySessionManagement sessionManagement = new StudySessionManagement();
            //add the study session to the database
            await sessionManagement.AddSession(session);

            //if the user studies more than is required then set the hours left
            //that they need to study to 0
            if (moduleToUpdate.HoursStudied > moduleToUpdate.SelfStudyHours)
            {
                moduleToUpdate.HoursLeft = 0;
            }
            else
            {
                //otherwise, subtract the times
                moduleToUpdate.HoursLeft = moduleToUpdate.SelfStudyHours - moduleToUpdate.HoursStudied;
            }

            //update the module in the database
            appDataContext.Modules.Update(moduleToUpdate);
            await appDataContext.SaveChangesAsync();
        }

        public List<Module> GetModules(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get a list of all the modules for the user id
            return appDataContext.Modules.Where(m => m.UserId == userID).ToList();
        }

        

        public bool ModuleExists(string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if the ucrrent user has already entered the module before
            return appDataContext.Modules.Any(m => m.ModuleCode == moduleCode && m.UserId == userID);
        }

        public long CalculateSelfStudyHours(Module module)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get the weeks in the semester from the database
            decimal weeks = appDataContext.UserSemesters.First(u => u.UserId == module.UserId).WeeksInSemester;
            
            //Calculation for the amount of time the student needs to self study
            decimal studyHours = ((module.Credits * 10 / weeks) - module.ClassHours);   
            if (studyHours < 0)
            {
                studyHours = 0;
            }
            //convert the calculation to a timespan to get the accurate time
            TimeSpan hours = TimeSpan.FromHours(Convert.ToDouble(studyHours));

            //return the long type to be stored in the database
            return hours.Ticks;

        }

    }
}
