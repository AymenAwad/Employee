namespace Shared.DTOs.Account
{
    using System;
    using System.Collections.Generic;

    public class AuthenticationResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsOwner { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public bool Success { get; set; }
        //public List<string> Roles { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>(); 
        //public Guid DefualtUserProject { get; set; }


    }
}
