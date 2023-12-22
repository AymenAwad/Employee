using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Base
{
    public class ClassBase
    {

        public ClassBase()
        {
        }
        public ClassBase(int val, string name)
        {
            Name = name;
            Id = val;
        }
        public ClassBase(int val, string name, string desc)
        {
            Name = name;
            Id = val;
            Description = desc;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public string Description { get; set; }
        public string NameAr { get; set; }
        public bool IsActive { get; set; }
        public string Text => Name;
    }

    public class ClassBase<T>
    {
        [Key]
        public T Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public bool Status { get; set; } = true;

    }
}

