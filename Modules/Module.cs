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
        private Nullable<DateTime> dateLastStudied;

        public Module(string moduleCode, string moduleName, double credits, double classHoursPerWeek, double numberOfWeeksInSemester, DateTime semesterStartDate)
        {
            this.moduleCode = moduleCode;
            this.moduleName = moduleName;
            this.credits = credits;
            this.classHoursPerWeek = classHoursPerWeek;
            this.numberOfWeeksInSemester = numberOfWeeksInSemester;
            this.semesterStartDate = semesterStartDate;
            //Initialize the study hours to zero becuase the user just added the module
            this.hoursStudied = TimeSpan.Zero;
            //Set the value of self study hours to the return value of the method
            this.selfStudyHours = CalculateSelfStudyHours();
            this.dateLastStudied = null;
           
        }
      

        public string ModuleCode { get => moduleCode; set => moduleCode = value; }
        public string ModuleName { get => moduleName; set => moduleName = value; }
        public double Credits { get => credits; set => credits = value; }
        public double ClassHoursPerWeek { get => classHoursPerWeek; set => classHoursPerWeek = value; }
        public double NumberOfWeeksInSemester { get => numberOfWeeksInSemester; set => numberOfWeeksInSemester = value; }
        public DateTime SemesterStartDate { get => semesterStartDate; set => semesterStartDate = value; }
        public TimeSpan SelfStudyHours { get => selfStudyHours; set => selfStudyHours = value; }
        public TimeSpan HoursStudied { get => hoursStudied; set => hoursStudied = value; }
        public TimeSpan HoursLeft { get => hoursLeft; set => hoursLeft = value; }
        public Nullable<DateTime> DateLastStudied { get => dateLastStudied; set => dateLastStudied = value; }

        public TimeSpan CalculateSelfStudyHours()
        {
            //Calculation for the amount of time the student needs to self study
            double studyHours = ((Credits * 10) / NumberOfWeeksInSemester) - ClassHoursPerWeek;
       
            //Convert the value to a TimeSpan so a manual calculation isn't necessary
            //The value sent as a parameter is not rounded off becuase it will allow the time to be
            //more accurate.
            return TimeSpan.FromHours(studyHours);

        }

        
    }

}