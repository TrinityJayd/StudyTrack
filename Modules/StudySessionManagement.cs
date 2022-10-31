using Modules.Models;

namespace Modules
{
    public class StudySessionManagement
    {
        public async Task AddSession(StudySession session)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            int totalSessions = appDataContext.StudySessions.Count();
            session.SessionId = totalSessions + 1;
            appDataContext.StudySessions.Add(session);
            await appDataContext.SaveChangesAsync();
        }
    }
}
