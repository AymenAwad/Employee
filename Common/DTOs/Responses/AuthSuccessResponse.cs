namespace Shared.DTOs.Responses
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}