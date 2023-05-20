namespace Puzzle.Lib.Session.Services
{
    /// <summary>
    /// Defines methods to interact with the session data.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Gets the value associated with the specified key from the session data and deserializes it to an object of type TData.
        /// </summary>
        /// <typeparam name="TData">The type of the object to deserialize the value to.</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The object of type TData associated with the specified key, or a new instance of TData if the key is not found.</returns>
        TData Get<TData>(string key);

        /// <summary>
        /// Sets the specified value to the session data associated with the specified key after serializing it.
        /// </summary>
        /// <typeparam name="TData">The type of the object to serialize and set.</typeparam>
        /// <param name="key">The key to set the value for.</param>
        /// <param name="data">The value to set after serializing it.</param>
        void Set<TData>(string key, TData data);

        /// <summary>
        /// Removes the value associated with the specified key from the session data.
        /// </summary>
        /// <param name="key">The key of the value to remove.</param>
        void Remove(string key);
    }
}
