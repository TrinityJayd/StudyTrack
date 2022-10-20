using System;
using System.Collections.Generic;

namespace Modules.Models
{
    public partial class Module
    {
        public int EntryId { get; set; }
        public string ModuleCode { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public decimal Credits { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public decimal WeeksInSemester { get; set; }
        public decimal ClassHours { get; set; }
        public long SelfStudyHours { get; set; }
        public long HoursStudied { get; set; }
        public long HoursLeft { get; set; }
        public DateTime? DateLastStudied { get; set; }
        public int? UserId { get; set; }

        public Module(string moduleCode, string moduleName, decimal credits, DateTime semesterStartDate, decimal weeksInSemester, decimal classHours)
        {
            ModuleCode = moduleCode;
            ModuleName = moduleName;
            Credits = credits;
            SemesterStartDate = semesterStartDate;
            WeeksInSemester = weeksInSemester;
            ClassHours = classHours;
            HoursStudied = 0;
            SelfStudyHours = CalculateSelfStudyHours();
            DateLastStudied = null; 
        }
        
        public long CalculateSelfStudyHours()
        {
            //Calculation for the amount of time the student needs to self study
            long studyHours = (long)(((Credits * 10) / WeeksInSemester) - ClassHours);

            //Convert the value to a TimeSpan so a manual calculation isn't necessary
            //The value sent as a parameter is not rounded off becuase it will allow the time to be
            //more accurate.
            return studyHours;

        }

        public virtual User? User { get; set; }
    }
}
