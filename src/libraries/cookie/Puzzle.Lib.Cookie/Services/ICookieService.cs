namespace Puzzle.Lib.Cookie.Services
{
    /// <summary>
    /// Defines methods for working with HTTP cookies.
    /// </summary>
    public interface ICookieService
    {
        /// <summary>
        /// Retrieves the data stored with the given key.
        /// </summary>
        /// <typeparam name="TData">The type of the stored data.</typeparam>
        /// <param name="key">The key of the stored data.</param>
        /// <returns>The value of the stored data.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the key parameter is null or empty</exception>
        TData Get<TData>(string key);

        /// <summary>
        /// Creates a new cookie with the given key and value, or updates the value of an existing one.
        /// </summary>
        /// <typeparam name="TData">The type of the data to be stored.</typeparam>
        /// <param name="key">The key under which the data will be stored.</param>
        /// <param name="value">The value of the data to be stored.</param>
        /// <exception cref="ArgumentNullException">Thrown when the key or value parameter is null.</exception>
        void Set<TData>(string key, TData value);

        /// <summary>
        /// Stores the given data with the specified key and expiration date.
        /// </summary>
        /// <typeparam name="TData">The type of the data to be stored.</typeparam>
        /// <param name="key">The key under which the data will be stored.</param>
        /// <param name="value">The data to be stored.</param>
        /// <param name="expireDate">The expiration date of the stored data.</param>
        /// <exception cref="ArgumentNullException">Thrown when the key parameter is null.</exception>
        void Set<TData>(string key, TData value, DateTime expireDate);

        /// <summary>
        /// Removes the data stored with the given key.
        /// </summary>
        /// <param name="key">The key of the data to be removed.</param>
        /// <exception cref="ArgumentNullException">Thrown when the key parameter is null.</exception>
        void Remove(string key);
    }
}
