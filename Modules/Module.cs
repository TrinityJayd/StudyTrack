using System.ComponentModel;

namespace Modules
{
    public class Module
    {
        private string moduleCode;
        private string moduleName;
        private int credits;
        private int classHoursPerWeek;
        private int numberOfWeekInSemester;
        private DateTime semesterStartDate;

        public Module(string moduleCode, string moduleName, int credits, int classHoursPerWeek, int numberOfWeekInSemester, DateTime semesterStartDate)
        {
            this.moduleCode = moduleCode;
            this.moduleName = moduleName;
            this.credits = credits;
            this.classHoursPerWeek = classHoursPerWeek;
            this.numberOfWeekInSemester = numberOfWeekInSemester;
            this.semesterStartDate = semesterStartDate;
        }

        public string ModuleCode { get => moduleCode; set => moduleCode = value; }

        [DisplayName("Module Name")]
        public string ModuleName { get => moduleName; set => moduleName = value; }
        public int Credits { get => credits; set => credits = value; }
        [DisplayName("Class Hours Per Week")]
        public int ClassHoursPerWeek { get => classHoursPerWeek; set => classHoursPerWeek = value; }
        public int NumberOfWeekInSemester { get => numberOfWeekInSemester; set => numberOfWeekInSemester = value; }
        public DateTime SemesterStartDate { get => semesterStartDate; set => semesterStartDate = value; }
    }
}