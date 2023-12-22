using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Auth
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserTypeId { get; set; }
        public int ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        //public List<RolesAndPermissionsOrganizationUserDto> RolesAndPermissions { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public string JWToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }


    #region Role and Permissions
    public class RolesAndPermissionsOrganizationUserDto
    {
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionKey { get; set; }

    }


    #endregion
}

