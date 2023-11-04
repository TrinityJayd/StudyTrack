using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class DGSession
    {
        //Class that models the study sessions index page 
        public string ModuleCode { get; set; }
        public TimeSpan TimeStudied { get; set; }
        public DateTime? DateStudied { get; set; }
        public string ModCode { get; set; }
    }
}
