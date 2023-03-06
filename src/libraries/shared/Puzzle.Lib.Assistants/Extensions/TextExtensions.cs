namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// Text extensions
    /// </summary>
    public static class TextExtensions
    {
        /// <summary>
        /// To title case with turkish culture
        /// </summary>
        /// <param name="text">text</param>
        /// <returns>converted title case</returns>
        public static string ToTitleCase(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
        }

        /// <summary>
        /// To clear spaces
        /// </summary>
        /// <param name="text">text to be converted</param>
        /// <returns>whitespace cleared text</returns>
        public static string ToClearSpaces(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return default;

            return text.Replace(" ", string.Empty).Trim();
        }
    }
}
