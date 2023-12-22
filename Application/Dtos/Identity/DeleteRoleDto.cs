using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.Identity
{
    public class DeleteRoleDto
    {
        [JsonIgnore]
        [Required]
        public string Id { get; set; }

        [Required]
        public int Status { get; set; }

        [JsonIgnore]
        public string ModifiedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
