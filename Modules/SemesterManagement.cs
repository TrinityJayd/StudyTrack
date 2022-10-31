using Modules.Models;

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

        public async Task UpdateSemester(int userID,DateTime startDate, decimal weeks)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            var semesterToUpdate = appDataContext.UserSemesters.Single(s => s.UserId == userID);
            semesterToUpdate.SemesterStartDate = startDate;
            semesterToUpdate.WeeksInSemester = weeks;
            appDataContext.UserSemesters.Update(semesterToUpdate);
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

        public bool SemesterExists(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            return appDataContext.UserSemesters.Any(s => s.UserId == userID);
        }
    }
}
