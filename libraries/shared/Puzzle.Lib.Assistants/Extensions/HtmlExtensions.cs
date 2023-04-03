namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// Provides extension methods for HTML related operations.
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Converts the given HTML text into plain text by removing all HTML tags.
        /// </summary>
        /// <param name="htmlText">The HTML text to convert.</param>
        /// <returns>The plain text version of the HTML text.</returns>
        public static string ConvertHtmlToText(this string htmlText)
        {
            if (string.IsNullOrWhiteSpace(htmlText))
                return string.Empty;

            return Regex.Replace(htmlText, "<[^>]*>", string.Empty);
        }
    }
}
