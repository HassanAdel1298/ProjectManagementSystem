using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ViewModel.Projects
{
    public class ProjectSearchViewModel
    {
        public string Name { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
