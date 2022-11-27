﻿using DbManagement;
using DbManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace Modules
{
    public class ModuleManagement
    {

        public async Task AddModule(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();

            //if the module exists and all the data matches just insert into the module entry table           
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

        }




        public async Task DeleteModule(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            int moduleID = module.ModuleId;
            //Remove the module from the module entry table
            appDataContext.ModuleEntries.Remove(appDataContext.ModuleEntries.Where(m => m.ModuleId == moduleID && m.UserId == userID).FirstOrDefault());

            //check if the user has any study sessions for the module
            string moduleCode = module.ModuleCode;
            if (appDataContext.StudySessions.Any(s => s.ModuleCode == moduleCode && s.UserId == userID))
            {
                //remove the study sessions from the study sessions table
                appDataContext.RemoveRange(appDataContext.StudySessions.Where(s => s.ModuleCode == moduleCode && s.UserId == userID));
            }
            await appDataContext.SaveChangesAsync();
        }

        public async Task AddStudySession(StudySessionHtmlModel session, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();

            StudySession studySession = new StudySession();
            studySession.HoursStudied = session.HoursStudied.Ticks;
            studySession.DateStudied = session.DateStudied;
            studySession.UserId = userID;
            studySession.ModuleCode = session.ModuleCode;

            StudySessionManagement sessionManagement = new StudySessionManagement();
            //add the study session to the database
            await sessionManagement.AddSession(studySession);

            var module = from m in appDataContext.Modules
                                 join me in appDataContext.ModuleEntries on m.ModuleId equals me.ModuleId
                                 where me.UserId == userID && m.ModuleCode == session.ModuleCode
                                 select m;

            var studyModule = from me in appDataContext.ModuleEntries
                              join m in appDataContext.Modules on me.ModuleId equals m.ModuleId
                              where me.UserId == userID && m.ModuleCode == session.ModuleCode
                              select me;
            studyModule.FirstOrDefault().HoursStudied += studySession.HoursStudied;
            //if the user studies more than is required then set the hours left
            //that they need to study to 0
            if (studySession.HoursStudied > module.FirstOrDefault().SelfStudyHours)
            {
                studyModule.FirstOrDefault().HoursLeft = 0;
            }
            else
            {
                studyModule.FirstOrDefault().HoursLeft -= studySession.HoursStudied;
            }
            

            //update the module in the database
            appDataContext.ModuleEntries.Update(studyModule.FirstOrDefault());
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
            var exists = appDataContext.Modules.Any(m => m.ModuleCode == module.ModuleCode && m.ModuleName == module.ModuleName
            && m.ClassHours == module.ClassHours && m.Credits == module.Credits);
            return exists;
        }

        public Module GetMatchingModule(Module module)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if a module in the database matches  the recieved module
            var exists = appDataContext.Modules.Where(m => m.ModuleCode == module.ModuleCode && m.ModuleName == module.ModuleName
            && m.ClassHours == module.ClassHours && m.Credits == module.Credits).FirstOrDefault();
            return exists;
        }

        public bool ModuleExistsInModuleEntry(Module module, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();

            var mod = GetMatchingModule(module);

            //check if there is a module in the module entry table that matches what the user has entered
            return appDataContext.ModuleEntries.Any(m => m.ModuleId == mod.ModuleId && m.UserId == userID);


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