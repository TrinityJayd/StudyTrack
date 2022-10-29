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
        
        public virtual User? User { get; set; }

        public long CalculateSelfStudyHours()
        {
            //Calculation for the amount of time the student needs to self study
            decimal studyHours = ((Credits * 10 / WeeksInSemester) - ClassHours);
            if (studyHours < 0)
            {
                studyHours = 0;
            }
            TimeSpan hours = TimeSpan.FromHours(Convert.ToDouble(studyHours));
            
            return hours.Ticks;

        }
    }
}
