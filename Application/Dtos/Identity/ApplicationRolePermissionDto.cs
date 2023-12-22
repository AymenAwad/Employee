using Domain.Entities.Identity;

namespace Application.Dtos.Identity
{
    public class ApplicationRolePermissionDto
    {
        public int ApplicationRoleId { get; set; }
      
        public string ApplicationRoleName { get; set; }

        public int ApplicationPermissionsId { get; set; }
        public string ApplicationPermissionsName { get; set; }
    }
}
