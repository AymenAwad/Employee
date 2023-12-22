namespace Application.Dtos.Identity
{
    public class RolesAndPermissionsApplicantDto
    {
        public int ApplicationRolePermissionId { get; set; }
        public int RoleId { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public int PermissionId { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionKey { get; set; }
        public int ApplicantId { get; set; }
        public int ApntPermissionId { get; set; }
        public string ApplicantNameAr { get; set; }
        public string ApplicantNameEn { get; set; }
        public int RoleStatus { get; set; }
        public int ApplicantStatus { get; set; }
    }
}
