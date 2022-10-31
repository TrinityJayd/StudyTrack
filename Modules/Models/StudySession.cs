using System;
using System.Collections.Generic;

namespace Modules.Models
{
    public partial class StudySession
    {
        public int SessionId { get; set; }
        public string ModuleCode { get; set; } = null!;
        public long HoursStudied { get; set; }
        public DateTime? DateStudied { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
