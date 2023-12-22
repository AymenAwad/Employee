using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.Identity
{
    public class CreatePermissionDto
    {
        // public int Id { get; set; }
        [Required]
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        [Required]
        public string Key { get; set; }
        
        [JsonIgnore]
        public string CreateBy { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public int Status { get; set; } = 1;
    }
}
