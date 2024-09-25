using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.TaskTimers
{
    public class TaskTimerUpdateDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public float DurationByMinute { get; set; }
        public int UserCreateID { get; set; }

    }
}
