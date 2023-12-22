
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Shared.Globalization
{
    public class StringLocalizer : IStringLocalizer
    {
        public LocalizedString this[string name] =>
            new LocalizedString(name, name);

        public LocalizedString this[string name, params object[] arguments] =>
            new LocalizedString(name, name);

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            throw new NotImplementedException();

        public IStringLocalizer WithCulture(CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
