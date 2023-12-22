using Domain.Entities.Application;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.Identity
{
    public class ApplicationPermission
    {
        [Key]
        public int Id { get; set; }

        public int PermissionId { get; set; }
        [ForeignKey(nameof(PermissionId))]
        public Permission Permission { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        public virtual ICollection<ApplicationRolePermission> ApplicationRolePermissions { get; set; }
    }
}