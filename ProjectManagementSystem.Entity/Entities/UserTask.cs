using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Entity.Entities
{
    public class UserTask : BaseModel
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }


        [ForeignKey("Task")]
        public int TaskID { get; set; }
        public AppTask Task { get; set; }

        public DateTime AssignedDate { get; set; }
    }
}
