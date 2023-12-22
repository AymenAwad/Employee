using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class CreateApplicationRoleDto
    {
        public string RoleId { get; set; }
        [Required]
        public int ApplicationId { get; set; } //AcademyId, OrganizationId, OR ApplicantId
        [Required]
        public int ApplicationTypeId { get; set; } //enum AcademyId = 2, OrganizationId = 1, or ApplicantId = 3
        [JsonIgnore]
        public string CreateBy { get; set; } = "admin";
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public int Status { get; set; } = 1;
    }
}
