using System;

namespace Shared.DTOs
{
    public class LookupDto
    {
        public string Id { get; set; } =string.Empty;
        public int ViewOrder { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}
