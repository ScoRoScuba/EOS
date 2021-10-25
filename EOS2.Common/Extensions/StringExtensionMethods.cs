namespace EOS2.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Threading;

    public static class StringExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Extension Method, can not be called directly")]
        public static void IfNotNull(this string target, Action<string> continuation)
        {
            if (target != null)
            {
                continuation(target);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Extension Method, variable name is descriptive of nature")]
        public static bool IsEmpty(this string stringValue)
        {
            return string.IsNullOrEmpty(stringValue);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Extension Method, variable name is descriptive of nature")]
        public static bool IsNotEmpty(this string stringValue)
        {
            return !string.IsNullOrEmpty(stringValue);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Extension Method, can not be called directly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Extension Method, variable name is descriptive of nature")]
        public static void IsNotEmpty(this string stringValue, Action<string> action)
        {
            if (stringValue.IsNotEmpty())
                action(stringValue);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Extension Method, variable name is descriptive of nature")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "bool", Justification = "Extension Method, variable name is descriptive of nature")]
        public static bool ToBool(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue)) return false;

            return bool.Parse(stringValue);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Extension Method, variable name is descriptive of nature")]
        public static string ToFormat(this string stringFormat, params object[] args)
        {
            return string.Format(CultureInfo.CurrentCulture, stringFormat, args);
        }

        /// <summary>
        /// Performs a case-insensitive comparison of strings
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension Method, can not be called directly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Extension Method, variable names are descriptive of nature")]
        public static bool EqualsIgnoreCase(this string thisString, string otherString)
        {
            return thisString.Equals(otherString, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Converts the string to Title Case
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Extension Method, variable name is descriptive of nature")]
        public static string Capitalize(this string stringValue)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringValue);
        }

        /// <summary>
        /// Formats a multi-line string for display on the web
        /// </summary>
        /// <param name="plainText"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "plainText", Justification = "Name emphasises purpose clearly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CRLF", Justification = "CRLF is normal format")]
        public static string ConvertCRLFToBreaks(this string plainText)
        {
            return new Regex("(\r\n|\n)").Replace(plainText, "<br/>");
        }

        /// <summary>
        /// Returns a DateTime value parsed from the <paramref name="dateTimeValue"/> parameter.
        /// </summary>
        /// <param name="dateTimeValue">A valid, parseable DateTime value</param>
        /// <returns>The parsed DateTime value</returns>
        public static DateTime ToDateTime(this string dateTimeValue)
        {
            return DateTime.Parse(dateTimeValue, CultureInfo.CurrentCulture);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gmt", Justification = "How else do you spell Gmt")]
        public static string ToGmtFormattedDate(this DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd hh':'mm':'ss tt 'GMT'", CultureInfo.CurrentCulture);
        }

        public static string[] ToDelimitedArray(this string content)
        {
            return content.ToDelimitedArray(',');
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Extension Method, can not be called directly")]
        public static string[] ToDelimitedArray(this string content, char delimiter)
        {
            string[] array = content.Split(delimiter);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Trim();
            }

            return array;
        }

        public static bool IsValidNumber(this string number)
        {
            return IsValidNumber(number, Thread.CurrentThread.CurrentCulture);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Extension Method, can not be called directly")]
        public static bool IsValidNumber(this string number, CultureInfo culture)
        {
            string validNumberPattern =
            @"^-?(?:\d+|\d{1,3}(?:" 
            + culture.NumberFormat.NumberGroupSeparator + 
            @"\d{3})+)?(?:\" 
            + culture.NumberFormat.NumberDecimalSeparator + 
            @"\d+)?$";

            return new Regex(validNumberPattern, RegexOptions.ECMAScript).IsMatch(number);
        }
        
        /// <summary>
        /// Reads text and returns an enumerable of strings for each line
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadLines(this string text)
        {
            var reader = new StringReader(text);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        }

        /// <summary>
        /// Reads text and calls back for each line of text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Justification = "Extension Method, can not be called directly")]
        public static void ReadLines(this string text, Action<string> callback)
        {
            using (var reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    callback(line);
                }
            }
        }

        /// <summary>
        /// Splits a camel cased string into seperate words delimitted by a space
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Name emphasises purpose clearly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "stringValue", Justification = "Name emphasises purpose clearly")]
        public static string SplitCamelCase(this string stringValue)
        {
            return Regex.Replace(Regex.Replace(stringValue, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
        }

        /// <summary>
        /// Splits a pascal cased string into seperate words delimitted by a space
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "string", Justification = "Name emphasises purpose clearly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "stringValue", Justification = "Name emphasises purpose clearly")]
        public static string SplitPascalCase(this string stringValue)
        {
            return SplitCamelCase(stringValue);
        }

        public static TEnum ToEnum<TEnum>(this string text) where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new ArgumentException("{0} is not an Enum".ToFormat(enumType.Name));
            return (TEnum)Enum.Parse(enumType, text, true);
        }
    }
}
