using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.Identity
{
    public class DeleteApplicationRolePermissionDto
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
