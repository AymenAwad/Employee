using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Identity;

namespace Application.Dtos.Identity
{
    public class CreateUserApplicationRoleDto
    {
        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public int ApplicationRoleId { get; set; }

        [JsonIgnore]
        public string CreateBy { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public int Status { get; set; } = 1;


        //[JsonIgnore]
        //public string ModifiedBy { get; set; }
        //[JsonIgnore]
        //public DateTime? LastUpdateDate { get; set; }
    }
}
