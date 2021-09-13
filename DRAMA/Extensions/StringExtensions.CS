using System.Globalization;

namespace DRAMA.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts the input string to title case, except for words that are entirely in uppercase.
        /// </summary>
        public static string ToTitleCase(this string text, string culture = "en-GB")
            => text is not null ? new CultureInfo(culture, false).TextInfo.ToTitleCase(text) : null;

        /// <summary>
        ///     Trims leading and trailing white-space characters, and converts the text to uppercase.
        ///     <para>The intended use of this method is string comparison operations.</para>
        /// </summary>
        public static string Flatten(this string text)
            => text?.Trim().ToUpper();
    }
}