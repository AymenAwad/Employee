using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.Base;

namespace Domain.Entities.Identity
{
    public class Permission : EntityBase<int>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Key { get; set; }
        public virtual ICollection<ApplicationPermission> ApplicationPermissions { get; set; }
    }
}
