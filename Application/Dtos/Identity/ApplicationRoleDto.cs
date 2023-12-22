using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class ApplicationRoleDto
    {
        public int Id { get; set; }

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public string ApplicationType { get; set; }
        public string ApplicationId { get; set; }
        public string ApplicationName { get; set; }

        // === AcademyId, OrganizationId, or ApplicantId

        public string CreateBy { get; set; }

        public DateTime CreationDate { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public int Status { get; set; }

    }
}
