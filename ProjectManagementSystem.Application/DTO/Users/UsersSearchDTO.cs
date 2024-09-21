using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Users
{
    public class UsersSearchDTO
    {
        public string Name { get; set; }

        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
