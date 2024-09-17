using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Entity.Entities
{
    public class UserProject : BaseModel
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }


        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        public DateTime? AssignedDate { get; set; }

        public UserRole Role { get; set; }

    }

    public enum UserRole
    {
        Owner = 1,
        User = 2
    }

}
