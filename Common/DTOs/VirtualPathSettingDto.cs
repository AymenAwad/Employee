using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class VirtualPathSettingDto
    {
        public List<PathContent> VirtualPaths { get; set; } = new List<PathContent>();
    }
    public class PathContent
    {
        public string RealPath { get; set; } = string.Empty;
        public string RequestPath { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
    }
}
