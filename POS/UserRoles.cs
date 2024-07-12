﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class UserRoles
    {
        public string RoleName { get; set; }
    }

    public static class Roles
    {
        public static readonly UserRoles Admin = new UserRoles { RoleName = "Admin" };
        public static readonly UserRoles Cashier = new UserRoles { RoleName = "Cashier" };
    }
}
