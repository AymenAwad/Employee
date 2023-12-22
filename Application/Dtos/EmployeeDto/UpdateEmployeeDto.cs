using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.EmployeeDto
{
    public class UpdateEmployeeDto
    {
        [Required]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string Agency { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Category { get; set; }

        [JsonIgnore]
        public int Status { get; set; } = (int)StatusEnum.Active;

        [JsonIgnore]
        public string CreateBy { get; set; }
        [JsonIgnore]
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
        [JsonIgnore]
        public DateTime? LastUpdateDate { get; set; } = DateTime.Now;
    }
}
