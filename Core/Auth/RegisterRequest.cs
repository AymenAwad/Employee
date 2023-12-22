using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Auth
{
    public class RegisterRequest
    {
        //[Required]
        //public string FirstName { get; set; }

        //[Required]
        //public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        //[Required]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }

        [JsonIgnore]
        public string FullName { get; set; }

        //public string ProfileImage { get; set; }
        [Required]
        public string UserTypeId { get; set; } //Enum
        [JsonIgnore]
        public string ActivationCode { get; set; }

        //[Required]
        //public List<int> ApplicationRoleIds { get; set; }

        [JsonIgnore]
        public string CreateBy { get; set; }

        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public int Status { get; set; } = 0;
    }
}
