using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Base
{
    public class LookupEntityBase
    {
        public LookupEntityBase()
        {

        }
        public LookupEntityBase(int val, string name)
        {
            Name = name;
            Id = val;
        }
        public LookupEntityBase(int val, string name, string desc)
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

    public class LookupEntityBase<T>
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
