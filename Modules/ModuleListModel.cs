using DbManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DbManagement
{
    public class ModuleListModel
    {
        public string[] ModuleCode { get; set; }
        public IEnumerable<SelectListItem> Entries { get; set; }

    }
}
