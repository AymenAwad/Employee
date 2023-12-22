namespace Shared.DTOs.Account
{
    using System.ComponentModel.DataAnnotations;

    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
