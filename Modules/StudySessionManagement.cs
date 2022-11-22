using Microsoft.EntityFrameworkCore;
using DbManagement.Models;

namespace Modules
{
    public class StudySessionManagement
    {
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
