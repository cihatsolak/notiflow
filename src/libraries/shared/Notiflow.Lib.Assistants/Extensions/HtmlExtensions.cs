namespace Notiflow.Lib.Assistants.Extensions
{
    /// <summary>
    /// Html extensions
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Convert html to text
        /// </summary>
        /// <param name="htmlText">html text</param>
        /// <returns>plain text</returns>
        public static string ConvertHtmlToText(this string htmlText)
        {
            if (string.IsNullOrWhiteSpace(htmlText))
                return string.Empty;

            return Regex.Replace(htmlText, "<[^>]*>", string.Empty);
        }
    }
}
