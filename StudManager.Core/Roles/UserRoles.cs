using System;
using System.Collections.Generic;
using System.Text;

namespace StudManager.Core.Roles
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string SuperAdmin = Admin + "," + User;
    }
}
