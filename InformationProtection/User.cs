using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationProtection
{
    public class User
    {
        public readonly int Id = 0000;
        public string? Login { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
        public bool Enable { get; set; } = true;

        public User()
        {
            Id++;
        }
        
    }
    public enum Role
    {
        Admin,
        User
    }
}
