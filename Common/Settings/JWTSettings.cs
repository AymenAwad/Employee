namespace Shared.Settings
{
    using System;

    public class JWTSettings
    {
        public string AccessTokenSecret { get; set; } 
        public double AccessTokenExpirationMinutes { get; set; }

        public string Issuer { get; set; }
        public string Audience { get; set; } 

        public TimeSpan TokenLifetime { get; set; }

        public string RefreshTokenSecret { get; set; }
        public double RefreshTokenExpirationMinutes { get; set; }
        public double RefreshTokenExpirationHours => 24;

        public string Key { get; set; } 
        public double DurationInMinutes { get; set; }

    }
}
