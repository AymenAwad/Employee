using Domain.Common.Base;
using Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Application
{
    [Table("Employee", Schema = "Application")]
    public class Employee : EntityBase<int>
    {
        public string Name { get; set; }
        public string Agency { get; set; }
        public string Department { get; set; }
        public int Rating { get; set; }
        public int Year { get; set; }
        public string Category { get; set; }
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<ApplicationRole> ApplicationRoles { get; set; }
        public ICollection<ApplicationPermission> ApplicationPermissions { get; set; }

    } 
}
