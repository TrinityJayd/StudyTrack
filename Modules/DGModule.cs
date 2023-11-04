using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class DGModule
    {
        //Class that models the html page that shows the user their study hours and hours left 
        public string ModuleCode { get; set; }
        public TimeSpan SelfStudyHours { get; set; }
        public TimeSpan HoursStudied{ get; set; }
        public TimeSpan HoursLeft { get; set; } 
    }
}
