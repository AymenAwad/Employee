using Microsoft.Extensions.Localization;
using System.Reflection;
using Localization.Resources;

namespace Shared.Globalization
{
    public class LocService
    {
        private IStringLocalizer _localizer;
        public LocService()
        {

        }

        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedString(string key)
        {
            var str = _localizer[key];
            return str;
        }
        public string Back()
        {
            return string.Format("<i style='font-weight:600' class='{0} btn-back font-large-2 primary darken-4' data-toggle='tooltip' title='{1}' onclick='window.history.back()'></i>", _localizer["zmdi zmdi-arrow-left"], _localizer["Back"]);
        }
        public string Add(string key)
        {
            return string.Format("<i style='font-weight:600' class='ft-plus font-large-2 primary darken-4' data-toggle='tooltip' title='{0}' onclick=location.href='{1}'></i>", _localizer["Add"], key);
        }

        public IStringLocalizer GetLocalizer()
        {
            StringLocalizerFactory stringLocalizerFactory = new StringLocalizerFactory();
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            var localizer = stringLocalizerFactory.Create("SharedResource", assemblyName.Name);
            _localizer = localizer;
            return localizer;
        }
    }
}