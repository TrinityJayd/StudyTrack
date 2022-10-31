using Microsoft.EntityFrameworkCore;
using Modules.Models;

namespace Modules
{
    public class StudySessionManagement
    {
        public async Task AddSession(StudySession session)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            var sessionEntry = await appDataContext.StudySessions.OrderByDescending(x => x.SessionId).FirstOrDefaultAsync();
            int lastID;
            if (sessionEntry == null)
            {
                lastID = 0;
            }
            else
            {
                lastID = sessionEntry.SessionId;
            }

            session.SessionId = lastID + 1;
            appDataContext.StudySessions.Add(session);
            await appDataContext.SaveChangesAsync();
        }

        public List<StudySession> GetSessions(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            List<StudySession> sessions = appDataContext.StudySessions.Where(x => x.UserId == userID).ToList();
            return sessions;
        }
    }
}
