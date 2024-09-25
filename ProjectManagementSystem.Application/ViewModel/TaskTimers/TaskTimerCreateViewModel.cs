using ProjectManagementSystem.Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ViewModel.TaskTimers
{
    public class TaskTimerCreateViewModel
    {
        public string Title { get; set; }

        public float DurationByMinute { get; set; }

        public int TaskID { get; set; }

    }
}
