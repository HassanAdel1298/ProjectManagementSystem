using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Projects
{
    public class ProjectReturnViewDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public int NumUsers { get; set; }
        public int NumTasks { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
