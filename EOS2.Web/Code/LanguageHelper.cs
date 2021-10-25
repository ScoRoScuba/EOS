namespace EOS2.Web.Code
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using i18n;

    public static class LanguageHelper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Helper Method, should be a function")]
        public static IEnumerable<Language> GetAvailableLanguages()
        {
            return
                LanguageHelpers.GetAppLanguages()
                    .Select(x => new Language() { Key = x.Key, NativeNameTitleCase = x.Value.NativeNameTitleCase })
                    .OrderBy(x => x.Key);
        }

        public static bool IsLanguageValid(string languageTag)
        {
            // Check that langtag is either null, empty (means Auto) or one of our supported languages.
            if (!string.IsNullOrEmpty(languageTag))
            {
                var langs = GetAvailableLanguages();
                var found =
                    langs.Where(lang => string.Compare(lang.Key, languageTag, StringComparison.OrdinalIgnoreCase) == 0);

                if (found.Any())
                {
                    return LanguageTag.GetCachedInstance(languageTag).IsValid();
                }
            }

            return false;            
        }
    }
}