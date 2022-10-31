using Modules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Modules
{
    public class SemesterManagement
    {
        public async Task AddSemester(UserSemester semester)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            int totalSemesters = appDataContext.UserSemesters.Count();
            semester.SemesterId = totalSemesters + 1;
            appDataContext.UserSemesters.Add(semester);
            await appDataContext.SaveChangesAsync();
        }

        public DateTime GetSemesterStartDate(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            return appDataContext.UserSemesters.First(m => m.UserId == userID).SemesterStartDate;
        }

        public decimal GetWeeksInSemester(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            return appDataContext.UserSemesters.First(m => m.UserId == userID).WeeksInSemester;
        }
    }
}
