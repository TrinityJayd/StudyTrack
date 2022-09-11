namespace Modules
{
    public class Module
    {
        private string moduleCode;
        private string moduleName;
        private int credits;
        private int classHoursPerWeek;

        
        public Module(string moduleCode, string moduleName, int credits, int classHoursPerWeek)
        {
            this.ModuleCode = moduleCode;
            this.ModuleName = moduleName;
            this.Credits = credits;
            this.ClassHoursPerWeek = classHoursPerWeek;
        }

        public string ModuleCode { get => moduleCode; set => moduleCode = value; }
        public string ModuleName { get => moduleName; set => moduleName = value; }
        public int Credits { get => credits; set => credits = value; }
        public int ClassHoursPerWeek { get => classHoursPerWeek; set => classHoursPerWeek = value; }
    }
}