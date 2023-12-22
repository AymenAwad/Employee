namespace Shared.DTOs.Account
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsDomainUser { get; set; }
    }
}
