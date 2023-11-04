using System;
using System.Collections.Generic;

namespace DbManagement.Models
{
    public partial class ModuleEntry
    {
        public int EntryId { get; set; }
        public long HoursStudied { get; set; }
        public long HoursLeft { get; set; }
        public int? ModuleId { get; set; }
        public int? UserId { get; set; }

        public virtual Module? Module { get; set; }
        public virtual User? User { get; set; }
    }
}
