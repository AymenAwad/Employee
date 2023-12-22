using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class CreateApplicationPermissionDto
    {
        //public int Id { get; set; }
        [Required]
        public int PermissionId { get; set; }
        [Required]
        public int? ApplicationId { get; set; } //AcademyId, OrganizationId, OR ApplicantId
        [Required]
        public int ApplicationTypeId { get; set; } //enum AcademyId = 2, OrganizationId = 1, or ApplicantId = 3



    }
}
