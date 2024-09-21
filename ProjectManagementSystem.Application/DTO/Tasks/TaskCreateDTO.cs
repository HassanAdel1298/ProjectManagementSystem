using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Tasks
{
    public class TaskCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime AssignedDate { get; set; }
        public int ProjectID { get; set; }
        public int UserCreateID { get; set; }
        public int UserAssignID { get; set; }

    }
}
