using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Entity.Entities
{
    public class TaskTimer : BaseModel
    {
        public string Title { get; set; }

        public float DurationByMinute { get; set; }

        [ForeignKey("Task")]
        public int TaskID { get; set; }
        public AppTask Task { get; set; }
    }
}
