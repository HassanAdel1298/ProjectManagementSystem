using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.UserProjects
{
    public class AssignProjectDTO
    {
        public int UserCreateID { get; set; }
        public int UserAssignID { get; set; }
        public int ProjectID { get; set; }
    }
}
