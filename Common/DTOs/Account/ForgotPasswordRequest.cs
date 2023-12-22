namespace Shared.DTOs.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ForgotPasswordRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        //[EmailAddress]
        //public string Email { get; set; }

    }
}
