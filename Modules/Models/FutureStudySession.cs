using System;
using System.Collections.Generic;

namespace DbManagement.Models
{
    public partial class FutureStudySession
    {
        public int FutureId { get; set; }
        public int? UserId { get; set; }
        public int? ModuleId { get; set; }
        public DateTime DateToStudy { get; set; }

        public virtual Module? Module { get; set; }
        public virtual User? User { get; set; }
    }
}
