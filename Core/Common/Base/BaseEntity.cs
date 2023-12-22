using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Base
{
    public class EntityBase<T>
    {
        [Key]
        public T Id { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; } = 1;
    }
}
