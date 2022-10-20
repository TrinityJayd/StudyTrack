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
            appDataContext.Modules.Add(module);
            await appDataContext.SaveChangesAsync();
        }

        public async Task DeleteModule(string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            appDataContext.Remove(appDataContext.Modules.Single(m => m.ModuleCode == moduleCode && m.UserId == userID));  
            await appDataContext.SaveChangesAsync();
        }

        public void UpdateModule(long hoursStudied, DateTime dateLastStudied, string moduleCode, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            var moduleToUpdate = appDataContext.Modules.Single(m => m.ModuleCode == moduleCode && m.UserId == userID);
            moduleToUpdate.HoursStudied += hoursStudied;
            moduleToUpdate.DateLastStudied = dateLastStudied;
            
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
            appDataContext.SaveChangesAsync();
        }

        public List<Module> GetModules(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            return appDataContext.Modules.Where(m => m.UserId == userID).ToList();
        }
    }
}
