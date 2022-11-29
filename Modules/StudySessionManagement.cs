using Microsoft.EntityFrameworkCore;
using DbManagement.Models;
using DbManagement;

namespace Modules
{
    public class StudySessionManagement
    {
        public async Task  ConvertStudySession(StudySessionHtmlModel session, int userID)
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
            if (studySession.HoursStudied >= module.FirstOrDefault().SelfStudyHours)
            {
                studyModule.FirstOrDefault().HoursLeft = 0;
            }
            else
            {
                studyModule.FirstOrDefault().HoursLeft = studyModule.FirstOrDefault().HoursLeft - studySession.HoursStudied;
                if (studyModule.FirstOrDefault().HoursLeft < 0)
                {
                    studyModule.FirstOrDefault().HoursLeft = 0;
                }
            }


            //update the module in the database
            appDataContext.ModuleEntries.Update(studyModule.FirstOrDefault());
            await appDataContext.SaveChangesAsync();
        }
        public async Task AddSession(StudySession session)
        {         
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get the value of the last session id entered
            var lastEntry = await appDataContext.StudySessions.OrderByDescending(x => x.SessionId).FirstOrDefaultAsync();
            if (lastEntry == null)
            {
                session.SessionId = 1;
            }
            else
            {
                session.SessionId = lastEntry.SessionId + 1;
            }
            //add the session to the databse
            appDataContext.StudySessions.Add(session);
            await appDataContext.SaveChangesAsync();
        }

        public List<StudySession> GetSessions(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get all study sessions for the current user
            List<StudySession> sessions = appDataContext.StudySessions.Where(x => x.UserId == userID).ToList();
            return sessions;
        }
    }
}
