namespace EOS2.Web
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;

    // ReSharper disable once InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public static class InternationalizationConfig
    {
        public static void Initialize()
        {
            i18n.LocalizedApplication.Current.DefaultLanguage = "en-US";

            // Change from the of temporary redirects during URL localization
            i18n.LocalizedApplication.Current.PermanentRedirects = true;

            // This line can be used to disable URL Localization.
            i18n.LocalizedApplication.Current.EarlyUrlLocalizerService = null;

            // Use scheme 2 which doesn't put a language code in the URL.
            i18n.UrlLocalizer.UrlLocalizationScheme = i18n.UrlLocalizationScheme.Scheme2;

            // Blacklist certain URLs from being 'localized'.
            i18n.UrlLocalizer.IncomingUrlFilters += delegate(Uri url)
            {
                if (url.LocalPath.EndsWith("sitemap.xml", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                return true;
            };

            // Allow I18n of base validation message(s)
            DefaultModelBinder.ResourceClassKey = "Validation";
        }
    }
}