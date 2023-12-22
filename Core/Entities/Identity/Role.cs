using Abp;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Shared.Exceptions;

namespace Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public string NameAr { get; set; }
        public bool IsDefualt { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; } = 1;

        public Role()
        {

        }

        public Role([NotNull] string name, string nameAr, bool isDefualt, string createBy, DateTime creationDate)
        {
            Shared.Exceptions.Check.NotNull(name, nameof(name));
            Id = Guid.NewGuid().AsSequentialGuid().ToString();
            Name = name;
            NameAr = nameAr;
            IsDefualt = isDefualt;
            CreateBy = createBy;
            NormalizedName = name.ToUpperInvariant();
        }
        public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
    }
}
