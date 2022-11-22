using System;
using System.Collections.Generic;

namespace DbManagement.Models
{
    public partial class Module
    {
        public Module()
        {
            ModuleEntries = new HashSet<ModuleEntry>();
        }

        public int ModuleId { get; set; }
        public string ModuleCode { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public decimal Credits { get; set; }
        public decimal ClassHours { get; set; }
        public long SelfStudyHours { get; set; }

        public virtual ICollection<ModuleEntry> ModuleEntries { get; set; }
    }
}
