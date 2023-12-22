using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum Roles
    {
        // General
        Admin,
        User,
        Employee,
    }
    public static class Constants
    {
        public static readonly string Admin = Guid.NewGuid().ToString();
        public static readonly string User = Guid.NewGuid().ToString();
        public static readonly string Employee = Guid.NewGuid().ToString();
    }
}
