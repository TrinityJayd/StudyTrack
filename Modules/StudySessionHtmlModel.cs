using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagement
{
    public class StudySessionHtmlModel
    {
        //modules the page that shows students all their study sessions
        public string ModuleCode { get; set; } = null!;
        public TimeSpan HoursStudied { get; set; }
        public DateTime? DateStudied { get; set; }
    }
}
