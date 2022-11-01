﻿using System;
using System.Collections.Generic;

namespace Modules.Models
{
    public partial class Module
    {
        public int EntryId { get; set; }
        public string ModuleCode { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public decimal Credits { get; set; }
        public decimal ClassHours { get; set; }
        public long SelfStudyHours { get; set; }
        public long HoursStudied { get; set; }
        public long HoursLeft { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
