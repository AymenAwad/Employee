using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class PermissionDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }        
        public string CreateBy { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string LastUpdateDate { get; set; }
        public string Status { get; set; }
    }
}
