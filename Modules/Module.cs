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
        private TimeSpan selfStudyHours;
        private TimeSpan hoursStudied;
        private TimeSpan hoursLeft;

        public Module(string moduleCode, string moduleName, double credits, double classHoursPerWeek, double numberOfWeeksInSemester, DateTime semesterStartDate)
        {
            this.moduleCode = moduleCode;
            this.moduleName = moduleName;
            this.credits = credits;
            this.classHoursPerWeek = classHoursPerWeek;
            this.numberOfWeeksInSemester = numberOfWeeksInSemester;
            this.semesterStartDate = semesterStartDate;
            this.hoursStudied = TimeSpan.Zero;
            this.selfStudyHours = calculateSelfStudyHours();
           
        }
      

        public string ModuleCode { get => moduleCode; set => moduleCode = value; }
        public string ModuleName { get => moduleName; set => moduleName = value; }
        public double Credits { get => credits; set => credits = value; }
        public double ClassHoursPerWeek { get => classHoursPerWeek; set => classHoursPerWeek = value; }
        public double NumberOfWeeksInSemester { get => numberOfWeeksInSemester; set => numberOfWeeksInSemester = value; }
        public DateTime SemesterStartDate { get => semesterStartDate; set => semesterStartDate = value; }
        public TimeSpan SelfStudyHours { get => selfStudyHours; set => selfStudyHours = calculateSelfStudyHours(); }
        public TimeSpan HoursStudied { get => hoursStudied; set => hoursStudied = value; }
        public TimeSpan HoursLeft { get => hoursLeft; set => hoursLeft = value; }

        public TimeSpan calculateSelfStudyHours()
        {
            double studyHours = ((Credits * 10) / NumberOfWeeksInSemester) - ClassHoursPerWeek;
       
            double roundedstudyHours = Math.Round(studyHours, 2);

            
            return TimeSpan.FromHours(roundedstudyHours);

        }

        
    }

}