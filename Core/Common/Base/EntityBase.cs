using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.Common.Base
    
{
    public class BaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
