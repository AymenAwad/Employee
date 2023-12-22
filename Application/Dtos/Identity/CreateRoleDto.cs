using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Shared.Exceptions;

namespace Application.Dtos.Identity
{
    public class CreateRoleDto
    {        
        [JsonIgnore, Required]
        public string Id { get; set; } = Guid.NewGuid().AsSequentialGuid().ToString();

        public string NameAr { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDefualt { get; set; }

        [JsonIgnore]
        public string CreateBy { get; set; }
        
        [JsonIgnore, Required]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
       
        [JsonIgnore, Required]
        public int Status { get; set; } = 1;
       
        [JsonIgnore]
        public string NormalizedName { get; set; } 
       
        [JsonIgnore]
        public string ConcurrencyStamp { get; set; }
    }
}
