using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Projects
{
    public class ProjectViewDTO
    {
        public int userID { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
