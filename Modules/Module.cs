namespace Modules
{
    public class Module
    {
        private string moduleCode;
        private string moduleName;
        private double credits;
        private double classHoursPerWeek;
        private double numberOfWeeksInSemester;
        private DateTime semesterStartDate;
        private double selfStudyHours;
        private double hoursStudied;

        public Module(string moduleCode, string moduleName, double credits, double classHoursPerWeek, double numberOfWeeksInSemester, DateTime semesterStartDate)
        {
            this.moduleCode = moduleCode;
            this.moduleName = moduleName;
            this.credits = credits;
            this.classHoursPerWeek = classHoursPerWeek;
            this.numberOfWeeksInSemester = numberOfWeeksInSemester;
            this.semesterStartDate = semesterStartDate;
            this.hoursStudied = 0;
            this.selfStudyHours = calculateSelfStudyHours();
        }
      

        public string ModuleCode { get => moduleCode; set => moduleCode = value; }
        public string ModuleName { get => moduleName; set => moduleName = value; }
        public double Credits { get => credits; set => credits = value; }
        public double ClassHoursPerWeek { get => classHoursPerWeek; set => classHoursPerWeek = value; }
        public double NumberOfWeeksInSemester { get => numberOfWeeksInSemester; set => numberOfWeeksInSemester = value; }
        public DateTime SemesterStartDate { get => semesterStartDate; set => semesterStartDate = value; }
        public double SelfStudyHours { get => selfStudyHours; set => selfStudyHours = value; }
        public double HoursStudied { get => hoursStudied; set => hoursStudied = value; }

        public double calculateSelfStudyHours()
        {
            double studyHours = ((Credits * 10) / NumberOfWeeksInSemester) - ClassHoursPerWeek;
       
            double roundedstudyHours = Math.Round(studyHours, 2);
            
            return roundedstudyHours;
        }
    }
}