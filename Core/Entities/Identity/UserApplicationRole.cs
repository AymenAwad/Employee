using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public class UserApplicationRole
    {
        [Key]
        public int Id { get; set; }
        
        public string ApplicationUserId { get; set; }
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public int ApplicationRoleId { get; set; }
        [ForeignKey(nameof(ApplicationRoleId))]
        public ApplicationRole ApplicationRole { get; set; }              

        public string CreateBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; } = 1;
    }
}
