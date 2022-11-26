﻿using DbManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace Modules
{
    public class ModuleManagement
    {

        public async Task AddModule(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();

            //if the module exists and all the data matches just insert into the module entry table
            if (ModuleExistsInDB(module))
            {
                await CreateModuleEntry(module, userID);
            }
            else
            {
                var lastEntry = await appDataContext.Modules.OrderByDescending(x => x.ModuleId).FirstOrDefaultAsync();

                if (lastEntry == null)
                {
                    module.ModuleId = 1;
                }
                else
                {
                    module.ModuleId = lastEntry.ModuleId + 1;
                }

                //calculate the self study hours
                module.SelfStudyHours = CalculateSelfStudyHours(module, userID);
                //add the module to the database
                appDataContext.Modules.Add(module);
                await appDataContext.SaveChangesAsync();
                await CreateModuleEntry(module, userID);
            }


        }

        public async Task DeleteModule(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            int moduleID = module.ModuleId;
            //Remove the module from the module entry table
            appDataContext.Remove(appDataContext.ModuleEntries.Where(m => m.ModuleId == moduleID && m.UserId == userID).FirstOrDefault());

            //check if the user has any study sessions for the module
            string moduleCode = module.ModuleCode;
            if (appDataContext.StudySessions.Any(s => s.ModuleCode == moduleCode && s.UserId == userID))
            {
                //remove the study sessions from the study sessions table
                appDataContext.RemoveRange(appDataContext.StudySessions.Where(s => s.ModuleCode == moduleCode && s.UserId == userID));
            }
            await appDataContext.SaveChangesAsync();
        }

        public async Task UpdateModule(long hoursStudied, DateTime dateLastStudied, Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            int moduleID = module.ModuleId;
            //Retrieve the module from the database
            var moduleToUpdate = appDataContext.ModuleEntries.Single(m => m.ModuleId == moduleID && m.UserId == userID);
            //accumulate the hours studied
            moduleToUpdate.HoursStudied += hoursStudied;
            string moduleCode = module.ModuleCode;
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
            if (moduleToUpdate.HoursStudied > module.SelfStudyHours)
            {
                moduleToUpdate.HoursLeft = 0;
            }
            else
            {
                //otherwise, subtract the times
                moduleToUpdate.HoursLeft = module.SelfStudyHours - moduleToUpdate.HoursStudied;
            }

            //update the module in the database
            appDataContext.ModuleEntries.Update(moduleToUpdate);
            await appDataContext.SaveChangesAsync();
        }

        public List<ModuleEntry> GetModules(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get a list of all the modules for the user id
            return appDataContext.ModuleEntries.Where(m => m.UserId == userID).ToList();
        }



        public bool ModuleExistsInDB(Module module)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if a module in the database matches  the recieved module
            return appDataContext.Modules.Contains(module);
        }

        public bool ModuleExistsInModuleEntry(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();

            //check if there is a module in the module entry table that matches what the user has entered
            return appDataContext.ModuleEntries.Any(m => m.ModuleId == module.ModuleId && m.UserId == userID);
        }

        public long CalculateSelfStudyHours(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get the weeks in the semester from the database
            decimal weeks = appDataContext.UserSemesters.First(u => u.UserId == userID).WeeksInSemester;

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

        public async Task CreateModuleEntry(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            ModuleEntry moduleEntry = new ModuleEntry();

            //Get the last entry id for a module
            var lastModuleEntry = await appDataContext.ModuleEntries.OrderByDescending(m => m.EntryId).FirstOrDefaultAsync();

            //if there are no modules in the table the id is 1
            if (lastModuleEntry == null)
            {
                moduleEntry.EntryId = 1;
            }
            else
            {
                moduleEntry.EntryId = lastModuleEntry.EntryId + 1;
            }


            moduleEntry.HoursStudied = 0;
            moduleEntry.HoursLeft = module.SelfStudyHours;
            moduleEntry.ModuleId = module.ModuleId;
            moduleEntry.UserId = userID;


            appDataContext.ModuleEntries.Add(moduleEntry);
            await appDataContext.SaveChangesAsync();
        }

    }
}
