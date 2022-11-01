using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class DGModule
    {
        //Class that models the datagrid on the HoursLeft user control
        public string ModuleCode { get; set; }
        public TimeSpan SelfStudyHours { get; set; }
        public TimeSpan HoursStudied{ get; set; }
        public TimeSpan HoursLeft { get; set; } 
    }
}
