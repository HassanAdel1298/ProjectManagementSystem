﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.DTO.Projects
{
    public class ProjectUpdateVisibilityDTO
    {
        public int ID { get; set; }
        public bool IsPublic { get; set; }
        public int UserCreateID { get; set; }
    }
}
