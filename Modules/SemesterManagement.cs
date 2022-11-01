using Microsoft.EntityFrameworkCore;
using Modules.Models;

namespace Modules
{
    public class SemesterManagement
    {
        public async Task AddSemester(UserSemester semester)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get the value of the last semester id entered
            var lastEntry = await appDataContext.UserSemesters.OrderByDescending(x => x.SemesterId).FirstOrDefaultAsync();

            if (lastEntry == null)
            {
                semester.SemesterId = 1;
            }
            else
            {
                semester.SemesterId = lastEntry.SemesterId + 1;
            };
            //add the semester to the database
            appDataContext.UserSemesters.Add(semester);
            await appDataContext.SaveChangesAsync();
        }

        public async Task UpdateSemester(int userID,DateTime startDate, decimal weeks)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //retrieve the semester from the database
            var semesterToUpdate = appDataContext.UserSemesters.Single(s => s.UserId == userID);
            //update semester details
            semesterToUpdate.SemesterStartDate = startDate;
            semesterToUpdate.WeeksInSemester = weeks;
            //update the database
            appDataContext.UserSemesters.Update(semesterToUpdate);
            await appDataContext.SaveChangesAsync();
        }

        public DateTime GetSemesterStartDate(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get the startdate of the users semester
            return appDataContext.UserSemesters.First(m => m.UserId == userID).SemesterStartDate;
        }

        public decimal GetWeeksInSemester(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //get the weeks of the users semester
            return appDataContext.UserSemesters.First(m => m.UserId == userID).WeeksInSemester;
        }

        public bool SemesterExists(int userID)
        {
            using Prog6212P2Context appDataContext = new Prog6212P2Context();
            //check if the user has already added a semester to the database
            return appDataContext.UserSemesters.Any(s => s.UserId == userID);
        }
    }
}
