namespace DRAMA.Extensions;

public static class StringExtensions
{
    /// <summary>
    ///     Converts the input string to title case, except for words that are entirely in uppercase.
    /// </summary>
    public static string ToTitleCase(this string text, string culture = "en-GB")
        => text is not null ? new CultureInfo(culture, false).TextInfo.ToTitleCase(text) : null;

    /// <summary>
    ///     Trims leading and trailing white-space characters, and converts the text to uppercase.
    ///     <br/>
    ///     The intended use of this method is string comparison operations.
    /// </summary>
    public static string Flatten(this string text)
        => text?.Trim().ToUpper();

    /// <summary>
    ///     Returns TRUE if the source string does not contain the provided text. Otherwise, returns FALSE.
    /// </summary>
    public static bool ContainsNot(this string text, string value)
        => !text.Contains(value);

    /// <summary>
    ///     Returns TRUE if the source string does not contain the provided text, while specifying the comparison type. Otherwise, returns FALSE.
    ///     <br/>
    ///     Valid string comparison types are the following: CurrentCulture, CurrentCultureIgnoreCase, InvariantCulture, InvariantCultureIgnoreCase, Ordinal, OrdinalIgnoreCase.
    /// </summary>
    public static bool ContainsNot(this string text, string value, StringComparison comparisonType)
        => !text.Contains(value, comparisonType);

    /// <summary>
    ///     Returns TRUE if the source string does not contain the provided character. Otherwise, returns FALSE.
    /// </summary>
    public static bool ContainsNot(this string text, char value)
        => !text.Contains(value);

    /// <summary>
    ///     Returns TRUE if the source string does not contain the provided character, while specifying the comparison type. Otherwise, returns FALSE.
    ///     <br/>
    ///     Valid string comparison types are the following: CurrentCulture, CurrentCultureIgnoreCase, InvariantCulture, InvariantCultureIgnoreCase, Ordinal, OrdinalIgnoreCase.
    /// </summary>
    public static bool ContainsNot(this string text, char value, StringComparison comparisonType)
        => !text.Contains(value, comparisonType);
}