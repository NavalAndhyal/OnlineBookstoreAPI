using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Models
{
    [Table("User2")]
    public class User
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; } // Store hashed password

        public string? FullName { get; set; }

        public Nullable<DateTime> DateOfBirth { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public int RoleId { get; set; } // Foreign key to role
        public Role? Role { get; set; }
    }
}
