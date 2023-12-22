using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum StatusEnum
    {
        Active = 1,
        Deleted = 2,
        UnVerified = 3,
        Suspended = 4,
        Rejected = 5,
        Cancelled = 6,
        Approved = 7,
        Pending = 8
    }
}
