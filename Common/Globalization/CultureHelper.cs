namespace Shared.Globalization
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using System;
    using System.Globalization;
    using System.Threading;
    using Shared.Constants;

    public static class CultureHelper
    {
        public static string CurrentCultureName => Thread.CurrentThread.CurrentCulture.Name;

        public static string CurrentDirection => IsRightToLeft ? "rtl" : "ltr";

        public static string CurrentLanguage => CultureInfo.CurrentCulture.Name.Substring(0, 2);

        public static bool IsArabic => CurrentLanguage.Equals("ar");

        public static bool IsRightToLeft => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;

        public static CultureInfo GetCultureInfo(string cultureName = "ar-SA")
        {
            // The default date in the system should be gregorian.
            var dateTimeFormat = new CultureInfo("en-GB").DateTimeFormat;

            // The default currency should be SAR
            var numberFormat = new CultureInfo("ar-SA").NumberFormat;
            numberFormat.CurrencyPositivePattern = 3;
            numberFormat.CurrencyNegativePattern = 3;

            // numberFormat.DigitSubstitution = DigitShapes.NativeNational;
            if (cultureName.Contains("|") && cultureName.Contains("="))
            {
                cultureName = cultureName.Split('|')[0].Split('=')[1];
            }

            return new CultureInfo(cultureName) { NumberFormat = numberFormat, DateTimeFormat = dateTimeFormat };
        }

        public static void InitializeCultureFromCookie(HttpContext context)
        {
            var defaultCulture = CommonsConfigurations.DefaultCulture;

            var cookieVal = context.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

            var cultureToSet = cookieVal ?? defaultCulture;
            var culture = GetCultureInfo(cultureToSet);

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            if (cookieVal == null)
            {
                context.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            }
        }
    }
}