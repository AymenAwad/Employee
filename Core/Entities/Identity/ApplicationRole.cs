using Domain.Entities.Application;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class ApplicationRole
    {
        [Key]
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public string RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; }
        public int? EmployeeId { get; set; }
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }
       
        public string CreateBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; } = 1;

        public virtual Collection<UserApplicationRole> UserApplicationRoles { get; set; }
       
    }
}
