﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Dal.Models.Site.Account
{
        public class RegisterViewModel
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PaswordConfirm { get; set; }

    }
}
