namespace Notiflow.Lib.Assistants.Extensions
{
    public static class InputExtensions
    {
        /// <summary>
        /// To clear unnecessary characters in number
        /// </summary>
        /// <param name="number">number</param>
        /// <returns>formatted number</returns>
        public static string ToClearUnnecessaryCharactersInNumber(this string number)
        {
            return number.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);
        }
    }
}
