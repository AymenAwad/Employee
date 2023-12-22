using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum UserTypeEnum
    {
        Employee = 1,
        Admin = 2,
        User = 3
       
    }

    public enum ApplicationTypeEnum
    {
        Employee = 1,
    }

    public enum PermissionTypeEnum
    {
        Add = 1,
        Update= 2,
        Delete= 3,
        View= 4,
    }
}
