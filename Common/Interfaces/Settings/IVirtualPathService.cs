using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Settings
{
    public interface IVirtualPathService
    {
        string GetContentsAttachementsRealPath_LocalDevelopment(string alias);
        string GetContentsAttachementsRequestPath_LocalDevelopment(string alias);

        string GetFesicalContentsAttachementsRequestPath(string alias);
        string GetFesicalContentsAttachementsRealPath(string alias);

        string GetFesicalProjectsImagesRequestPath(string alias);
        string GetFesicalProjectsImagesRealPath(string alias);

        string GetFesicalUserProfilesImagesRequestPath(string alias);
        string GetFesicalUserProfilesImagesRealPath(string alias);
    }
}
