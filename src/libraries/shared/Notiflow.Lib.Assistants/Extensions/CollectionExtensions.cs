namespace Notiflow.Lib.Assistants.Extensions
{
    /// <summary>
    /// Collection extensions
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Is null or not any
        /// </summary>
        /// <typeparam name="T">type of enumerable interface</typeparam>
        /// <param name="source">system collections generic</param>
        /// <returns>type of boolean</returns>
        public static bool IsNullOrNotAny<T>(this IEnumerable<T> source)
        {
            return !(source?.Any() ?? false);
        }

        /// <summary>
        /// Or empty if null
        /// </summary>
        /// <typeparam name="T">generic type</typeparam>
        /// <param name="source">type of IEnumerable interface</param>
        /// <returns>type of IEnumberable interface</returns>
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
    }
}
