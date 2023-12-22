using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class RolesAndPermissionsRestaurantyDto
    {
        public int ApplicationRolePermissionId { get; set; }
        public int RoleId { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public int PermissionId { get; set; }
        public string PermissionNameAr { get; set; }
        public string PermissionNameEn { get; set; }
        public string PermissionKey { get; set; }
        public int RestaurantId { get; set; }
        public int RestPermissionId { get; set; }
        public string RestaurantName { get; set; }
        public int RoleStatus { get; set; }
        public int RestaurantStatus { get; set; }
    }
}
