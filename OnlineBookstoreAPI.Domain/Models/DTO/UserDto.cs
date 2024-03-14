using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookstoreAPI.Domain.Models.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; } // Store hashed password

        public string? FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? RegistrationDate { get; set; }
        public int RoleId { get; set; } // Foreign key to role
        public Role? Role { get; set; }
    }
}
