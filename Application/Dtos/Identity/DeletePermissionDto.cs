using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.Identity
{
    public class DeletePermissionDto
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }

        [Required]
        public int Status { get; set; }

        [JsonIgnore]
        public string ModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastUpdateDate { get; set; } = DateTime.Now;
    }
}
