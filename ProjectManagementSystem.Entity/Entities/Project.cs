using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Entity.Entities
{
    public class Project : BaseModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreatedDate { get; set; }

        [ForeignKey("User")]
        public int? UserID { get; set; }
        public User? User { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }

        public ICollection<AppTask> Tasks { get; set; }
    }
}
