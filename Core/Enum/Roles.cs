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
        OrganizationDelegate,
        AcademyDelegate,
        Applicant,
        EmployeeTGA
    }
    public static class Constants
    {
        public static readonly string Admin = Guid.NewGuid().ToString();
        public static readonly string OrganizationDelegate = Guid.NewGuid().ToString();
        public static readonly string AcademyDelegate = Guid.NewGuid().ToString();
        public static readonly string Applicant = Guid.NewGuid().ToString();
        public static readonly string EmployeeTGA = Guid.NewGuid().ToString();
    }
}
