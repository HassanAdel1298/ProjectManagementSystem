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

        public TaskStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public Project Project { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User? User { get; set; }

        public ICollection<UserTask> UserTasks { get; set; }

    }

    public enum TaskStatus
    {
        Done = 1,
        InProgress = 2,
        ToDo = 3
    }
}
