using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Entity.Entities
{
    public class AppTask : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime AssignedDate { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        [ForeignKey("UserCreate")]
        public int? UserCreateID { get; set; }
        public User? UserCreate { get; set; }

        [ForeignKey("UserAssign")]
        public int? UserAssignID { get; set; }
        public User? UserAssign { get; set; }

        public ICollection<TaskTimer> TaskTimers { get; set; }
    }

    public enum Status
    {
        Done = 1,
        InProgress = 2,
        ToDo = 3
    }
}
