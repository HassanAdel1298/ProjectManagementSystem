﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Application.ViewModel.Users
{
    public class VerifyAccountViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }
    }
}
