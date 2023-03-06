namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// String comparison extensions
    /// </summary>
    public static class ComparisonExtensions
    {
        /// <summary>
        /// Büyük/küçük harfe duyarlı olmayan sıralı bir karşılaştırma gerçekleştirir.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="valueToCompare">value to compare</param>
        /// <seealso cref="https://docs.microsoft.com/tr-tr/dotnet/standard/base-types/best-practices-strings"/>
        /// <returns>type of boolean</returns>
        public static bool OrdinalIgnoreCase(this string value, string valueToCompare)
        {
            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(valueToCompare))
                return true;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Equals(valueToCompare, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Sıralı bir karşılaştırma gerçekleştirir.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="valueToCompare">value to compare</param>
        /// <seealso cref="https://docs.microsoft.com/tr-tr/dotnet/standard/base-types/best-practices-strings"/>
        /// <returns>type of boolean</returns>
        public static bool Ordinal(this string value, string valueToCompare)
        {
            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(valueToCompare))
                return true;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Equals(valueToCompare, StringComparison.Ordinal);
        }

        /// <summary>
        /// Kültüre duyarlı sıralama kurallarını, geçerli kültürü kullanarak ve karşılaştırılan dizelerin büyük/küçük harf durumunu yok sayarak dizeleri karşılaştırın.
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="valueToCompare">value to compare</param>
        /// <seealso cref="https://docs.microsoft.com/tr-tr/dotnet/standard/base-types/best-practices-strings"/>
        /// <returns>type of boolean</returns>
        public static bool CurrentCultureIgnoreCase(this string value, string valueToCompare)
        {
            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(valueToCompare))
                return true;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Equals(valueToCompare, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
