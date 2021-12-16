﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertificationMS.Models
{
    public class LoginModel
    {
        public string EmployeeId { get; set; }
        public string Password { get; set; }

    }
    public class LoginViewModel
    {
        public LoginModel user { get; set; }
        public string Message { get; set; }
    }
}
