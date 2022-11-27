using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagement
{
    public class StudySessionHtmlModel
    {
        public string ModuleCode { get; set; } = null!;
        public TimeSpan HoursStudied { get; set; }
        public DateTime? DateStudied { get; set; }
    }
}
