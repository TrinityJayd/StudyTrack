using System.Web.Mvc;

namespace DbManagement
{
    public class ModuleListModel
    {
        public string[] ModuleCode { get; set; }
        public IEnumerable<SelectListItem> Entries { get; set; }

    }
}
