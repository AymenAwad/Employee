namespace Shared.DTOs.Responses
{
    using System.Collections.Generic;

    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}