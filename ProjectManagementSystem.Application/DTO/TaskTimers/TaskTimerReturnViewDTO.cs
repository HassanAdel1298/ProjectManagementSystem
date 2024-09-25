using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.TaskTimers
{
    public class TaskTimerReturnViewDTO
    {
        public string Title { get; set; }

        public float DurationByMinute { get; set; }

        public string TaskName { get; set; }
    }
}
