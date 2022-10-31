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
            int totalModules = appDataContext.Modules.Count();
            module.EntryId = totalModules + 1;
            module.SelfStudyHours = CalculateSelfStudyHours(module);
            module.HoursLeft = module.SelfStudyHours;
            appDataContext.Modules.Add(module);
            await appDataContext.SaveChangesAsync();
        }

        public async Task DeleteModule(string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            appDataContext.Remove(appDataContext.Modules.Single(m => m.ModuleCode == moduleCode && m.UserId == userID));  
            await appDataContext.SaveChangesAsync();
        }

        public async Task UpdateModule(long hoursStudied, DateTime dateLastStudied, string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            var moduleToUpdate = appDataContext.Modules.Single(m => m.ModuleCode == moduleCode && m.UserId == userID);
            moduleToUpdate.HoursStudied += hoursStudied;

            StudySession session = new StudySession
            {
                UserId = userID,
                ModuleCode = moduleCode,
                HoursStudied = hoursStudied,
                DateStudied = dateLastStudied
            };

            StudySessionManagement sessionManagement = new StudySessionManagement();
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
            
            appDataContext.Modules.Update(moduleToUpdate);
            await appDataContext.SaveChangesAsync();
        }

        public List<Module> GetModules(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            return appDataContext.Modules.Where(m => m.UserId == userID).ToList();
        }

        

        public bool ModuleExists(string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            return appDataContext.Modules.Any(m => m.ModuleCode == moduleCode && m.UserId == userID);
        }

        public long CalculateSelfStudyHours(Module module)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            decimal weeks = appDataContext.UserSemesters.First(u => u.UserId == module.UserId).WeeksInSemester;
            
            //Calculation for the amount of time the student needs to self study
            decimal studyHours = ((module.Credits * 10 / weeks) - module.ClassHours);   
            if (studyHours < 0)
            {
                studyHours = 0;
            }
            TimeSpan hours = TimeSpan.FromHours(Convert.ToDouble(studyHours));

            return hours.Ticks;

        }

    }
}
