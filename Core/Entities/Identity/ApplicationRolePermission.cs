using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity
{
    public class ApplicationRolePermission
    {
        [Key]
        public int Id { get; set; }

        public int ApplicationRoleId { get; set; }
        [ForeignKey(nameof(ApplicationRoleId))]
        public ApplicationRole ApplicationRole { get; set; }

        public int ApplicationPermissionsId { get; set; }

        [ForeignKey(nameof(ApplicationPermissionsId))]
        public ApplicationPermission ApplicationPermission { get; set; }
    }
}
