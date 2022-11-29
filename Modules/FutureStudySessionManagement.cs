using DbManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace DbManagement
{
    public class FutureStudySessionManagement
    {
        public async Task ConvertFutureSession(FutureStudySessionHtmlModel session, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();

            FutureStudySession studySession = new FutureStudySession();
            studySession.UserId = userID;
            studySession.DateToStudy = session.DateToStudy;

            studySession.ModuleId = (from m in appDataContext.Modules
                                     join me in appDataContext.ModuleEntries on m.ModuleId equals me.ModuleId
                                     where me.UserId == userID && m.ModuleCode == session.ModuleCode
                                     select m.ModuleId).FirstOrDefaultAsync().Result;


            await AddFutureSession(studySession);
        }

        public async Task AddFutureSession(FutureStudySession session)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            var lastEntry = await appDataContext.FutureStudySessions.OrderByDescending(x => x.FutureId).FirstOrDefaultAsync();
            if (lastEntry == null)
            {
                session.FutureId = 1;
            }
            else
            {
                session.FutureId = lastEntry.FutureId + 1;
            }
            //add the session to the databse
            appDataContext.FutureStudySessions.Add(session);
            await appDataContext.SaveChangesAsync();
        }

        public bool CheckIfSessionAlreadyScheduled(DateTime dateToStudy, int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if there are any sessions with the same date as the date in the object
            bool exists = appDataContext.FutureStudySessions.Any(f => f.UserId == userID && f.DateToStudy == dateToStudy);
            return exists;
        }

        public string SessionToday(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if there are any sessions with the same date as the date in the object and return the module code
            
            string moduleCode = (from m in appDataContext.Modules
                                 join f in appDataContext.FutureStudySessions on m.ModuleId equals f.ModuleId
                                 where f.UserId == userID && f.DateToStudy == DateTime.Today
                                 select m.ModuleCode).FirstOrDefaultAsync().Result;

            if (moduleCode != null)
            {
                return $"You have a {moduleCode} study session scheduled for today.";
            }
            else
            {
                return "You have no study sessions scheduled for today.";
            }
        }
    }
}
