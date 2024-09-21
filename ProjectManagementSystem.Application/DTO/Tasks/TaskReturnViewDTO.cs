using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Tasks
{
    public class TaskReturnViewDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime AssignedDate { get; set; }
        public string ProjectName { get; set; }
        public string UserCreateName { get; set; }
        public string UserAssignName { get; set; }
    }
}
