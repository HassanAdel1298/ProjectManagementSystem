using System;
using System.Collections.Generic;
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
        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<AppTask> Tasks { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
    }
}
