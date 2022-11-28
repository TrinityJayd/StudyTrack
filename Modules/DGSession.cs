using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules
{
    public class DGSession
    {
        //Class that models the datagrid on the Session user control
        public string ModuleCode { get; set; }
        public TimeSpan TimeStudied { get; set; }
        public DateTime? DateStudied { get; set; }
        public string ModCode { get; set; }
    }
}
