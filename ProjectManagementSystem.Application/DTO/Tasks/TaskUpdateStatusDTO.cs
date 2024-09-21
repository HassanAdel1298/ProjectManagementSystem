using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Tasks
{
    public class TaskUpdateStatusDTO
    {
        public int ID { get; set; }
        public Status Status { get; set; }
        public int UserCreateID { get; set; }

    }
}
