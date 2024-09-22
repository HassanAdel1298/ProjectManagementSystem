using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Entity.Entities
{
    public class User : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
        public bool IsEmailVerified { get; set; } = false;
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<UserProject> UserProjects { get; set; }

        [InverseProperty("UserCreate")]
        public ICollection<AppTask> CreateTasks { get; set; }

        [InverseProperty("UserAssign")]
        public ICollection<AppTask> AssignTasks { get; set; }
    }
}
