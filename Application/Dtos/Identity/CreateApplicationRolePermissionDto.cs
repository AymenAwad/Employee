using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class CreateApplicationRolePermissionDto
    {
        [Required]
        public int ApplicationRoleId { get; set; }
        [Required]
        public int ApplicationPermissionsId { get; set; }
    }
}
