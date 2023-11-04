using System;
using System.Collections.Generic;

namespace DbManagement.Models
{
    public partial class UserSemester
    {
        public int SemesterId { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public decimal WeeksInSemester { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
