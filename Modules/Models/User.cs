using System;
using System.Collections.Generic;

namespace DbManagement.Models
{
    public partial class User
    {
        public User()
        {
            ModuleEntries = new HashSet<ModuleEntry>();
            StudySessions = new HashSet<StudySession>();
            UserSemesters = new HashSet<UserSemester>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string CellNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<ModuleEntry> ModuleEntries { get; set; }
        public virtual ICollection<StudySession> StudySessions { get; set; }
        public virtual ICollection<UserSemester> UserSemesters { get; set; }
    }
}
