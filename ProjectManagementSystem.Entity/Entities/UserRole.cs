using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Entity.Entities
{
    public class UserRole : BaseModel
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }


        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
