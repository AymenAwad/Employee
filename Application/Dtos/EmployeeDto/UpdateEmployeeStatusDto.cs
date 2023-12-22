using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.EmployeeDto
{
    public class UpdateEmployeeStatusDto
    {
        [JsonIgnore]
        [Required]
        public int Id { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
