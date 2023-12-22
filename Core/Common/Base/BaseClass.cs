using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Base
{
    public class BaseClass<T>
    {
        public T Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public bool IsActiv { get; set; } = true;
    }
}
