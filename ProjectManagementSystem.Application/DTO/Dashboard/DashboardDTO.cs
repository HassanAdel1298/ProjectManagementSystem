using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Dashboard
{
    public class DashboardDTO
    {

        public int TotalUsersActive { get; set; }
        public int TotalUsersInactive { get; set; }
        public int TotalProjects { get; set; }
        public int TotalTasks { get; set; }
        public int TotalTasksInProgress { get; set; }
    }
}
