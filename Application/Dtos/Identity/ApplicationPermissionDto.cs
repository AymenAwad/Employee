using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class ApplicationPermissionDto
    {
        public string Id { get; set; }

        public string PermissionId { get; set; }

        public string PermissionName { get; set; }
        public string PermissionKey { get; set; }

        // === EmployeeId
        public string ApplicationType { get; set; }
        public string ApplicationId { get; set; }
        public string ApplicationName { get; set; }
    }
}
