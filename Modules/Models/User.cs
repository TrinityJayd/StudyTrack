using System;
using System.Collections.Generic;

namespace Modules.Models
{
    public partial class User
    {
        public User()
        {
            Modules = new HashSet<Module>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string CellNumber { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Module> Modules { get; set; }
    }
}
