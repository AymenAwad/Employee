using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class RolesAndPermissionsOrganizationDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationNameAr { get; set; }
        public string OrganizationNameEn { get; set; }
        public int OrganizationStatus { get; set; }
        public int RoleId { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public int RoleStatus { get; set; }
        public int ApplicationRolePermissionId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionKey { get; set; }
        public int OrgPermissionId { get; set; }

        //public RolesAndPermissionsOrganizationDto()
        //{
        //    RoleOrganizations = new List<RoleOrganization>();
        //}
        //public int OrganizationId { get; set; }
        //public string OrganizationNameAr { get; set; }
        //public string OrganizationNameEn { get; set; }
        //public int OrganizationStatus { get; set; }

        //public List<RoleOrganization> RoleOrganizations { get; set; }

    }

    public class RoleOrganization
    {
        public RoleOrganization()
        {
            PermissionOrganizations = new List<PermissionOrganization>();
        }
        public int RoleId { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public int RoleStatus { get; set; }
        public List<PermissionOrganization> PermissionOrganizations { get; set; }
    }

    public class PermissionOrganization
    {
        public int ApplicationRolePermissionId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionKey { get; set; }
        public int OrgPermissionId { get; set; }
    }


    public class RolesAndPermissionsOrganizationResultDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationNameAr { get; set; }
        public string OrganizationNameEn { get; set; }
        public int OrganizationStatus { get; set; }
        public int RoleId { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public int RoleStatus { get; set; }
        public int ApplicationRolePermissionId { get; set; }
        public int PermissionId { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionKey { get; set; }
        public int OrgPermissionId { get; set; }
    }

    public class OrgRoles
    {
        public int OrganizationId { get; set; }
        public int RoleId { get; set; }
    }

}
