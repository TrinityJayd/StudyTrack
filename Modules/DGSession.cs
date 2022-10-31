using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class DGSession
    {
        public string ModuleCode { get; set; }
        public TimeSpan TimeStudied { get; set; }
        public DateTime? DateStudied { get; set; }
    }
}
