using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Shared.Interfaces.Services;

namespace Shared.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirst("uid").Value;
        }

        public string UserId { get; }
    }
}
