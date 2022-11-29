using DbManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagement
{
    public class FutureStudySessionManagement
    {
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
        
        public bool CheckIfSessionAlreadyScheduled(FutureStudySession session)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if there are any sessions with the same date as the date in the object
            bool exists = appDataContext.FutureStudySessions.Any(f => f.UserId == session.UserId && f.DateToStudy == session.DateToStudy);
            return exists;
        }
    }
}
