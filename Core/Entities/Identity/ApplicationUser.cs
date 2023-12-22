using Microsoft.AspNetCore.Identity;
using Domain.Auth;
using Domain.Entities.Application;

namespace Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
        public string UserTypeId { get; set; } //Enum
        public DateTime? LastChangePassword { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public string LastLoginStatus { get; set; }
        public string ActivationCode { get; set; }
        public bool IsLogedIn { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public int Status { get; set; } = 1;

        public virtual Employee Employee { get; set; }
        public virtual ICollection<UserApplicationRole> UserApplicationRoles { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
        public bool OwnsToken(string token)
        {
            return RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}