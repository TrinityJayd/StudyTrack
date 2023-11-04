using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagement
{
    public class ModuleHtmlModule
    {
        //model the page that shows the user what modules they have
        public int ModuleId { get; set; }
        public string ModuleCode { get; set; } = null!;
        public string ModuleName { get; set; } = null!;
        public int Credits { get; set; }
        public TimeSpan ClassHours { get; set; }
        public TimeSpan SelfStudyHours { get; set; }
    }
}
