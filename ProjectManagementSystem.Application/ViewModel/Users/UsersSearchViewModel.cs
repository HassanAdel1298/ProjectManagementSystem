using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ViewModel.Users
{
    public class UsersSearchViewModel
    {
        public string Name { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
