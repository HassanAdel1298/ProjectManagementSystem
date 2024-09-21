using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.UserProjects
{
    public class UnassignProjectDTO
    {
        public int UserCreateID { get; set; }
        public int UserUnassignID { get; set; }
        public int ProjectID { get; set; }
    }
}
