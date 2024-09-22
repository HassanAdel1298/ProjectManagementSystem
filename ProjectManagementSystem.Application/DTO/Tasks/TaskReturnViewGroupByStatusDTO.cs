using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Tasks
{
    public class TaskReturnViewGroupByStatusDTO
    {
        public List<TaskReturnViewDTO> Done { get; set; }
        public List<TaskReturnViewDTO> InProgress { get; set; }
        public List<TaskReturnViewDTO> ToDo { get; set; }
        
    }
}
