using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.Identity
{
    public class DeleteApplicationRoleDto
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [JsonIgnore]
        public string CurrentUserId { get; set; } = "admin";
        [Required]
        public int ApplicationId { get; set; } //AcademyId, OrganizationId, OR ApplicantId
        [Required]
        public int ApplicationTypeId { get; set; } //enum AcademyId = 2, OrganizationId = 1, or ApplicantId = 3

    }
}
