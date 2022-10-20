using System;
using System.Collections.Generic;

namespace Modules.Models
{
    public partial class Module
    {
        public int EntryId { get; set; }
        public string ModuleCode { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public int Credits { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public int WeeksInSemester { get; set; }
        public int ClassHours { get; set; }
        public long SelfStudyHours { get; set; }
        public long HoursStudied { get; set; }
        public long HoursLeft { get; set; }
        public DateTime DateLastStudied { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
