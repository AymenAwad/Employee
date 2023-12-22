using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DTOs;
using Shared.Interfaces.Settings;

namespace Shared.Settings
{

    public class VirtualPathService : IVirtualPathService
    {
        private readonly VirtualPathSettingDto _virtualPathSetting;

        public VirtualPathService(VirtualPathSettingDto virtualPathSetting)
        {
            _virtualPathSetting = virtualPathSetting;
        }

        public string GetContentsAttachementsRealPath_LocalDevelopment(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RealPath ?? "";
        }
        public string GetContentsAttachementsRequestPath_LocalDevelopment(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RequestPath ?? "";
        }

        public string GetFesicalContentsAttachementsRequestPath(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RequestPath ?? "";
        }
        public string GetFesicalContentsAttachementsRealPath(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RealPath ?? "";
        }

        public string GetFesicalProjectsImagesRequestPath(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RequestPath ?? "";
        }
        public string GetFesicalProjectsImagesRealPath(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RealPath ?? "";
        }

        public string GetFesicalUserProfilesImagesRequestPath(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RequestPath ?? "";
        }
        public string GetFesicalUserProfilesImagesRealPath(string alias)
        {
            return _virtualPathSetting.VirtualPaths.FirstOrDefault(x => x.Alias == alias)?.RealPath ?? "";
        }
    }
}
