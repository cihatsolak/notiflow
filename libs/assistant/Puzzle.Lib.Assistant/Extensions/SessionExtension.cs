namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Defines methods to interact with the session data.
/// </summary>
public static class SessionExtension
{
    /// <summary>
    /// Retrieves data of type <typeparamref name="TData"/> from the current session based on the specified key.
    /// </summary>
    /// <typeparam name="TData">The type of data to retrieve from the session.</typeparam>
    /// <param name="httpContext">The current HttpContext.</param>
    /// <param name="key">The key used to identify the data in the session.</param>
    /// <returns>The retrieved data, or the default value for the type if the key is not found.</returns>
    public static TData Get<TData>(this HttpContext httpContext, string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        bool isExists = httpContext.Session.TryGetValue(key, out byte[] value);
        if (!isExists)
            return default;

        return JsonSerializer.Deserialize<TData>(Encoding.UTF8.GetString(value));
    }

    /// <summary>
    /// Sets data of type <typeparamref name="TData"/> in the current session with the specified key.
    /// </summary>
    /// <typeparam name="TData">The type of data to store in the session.</typeparam>
    /// <param name="httpContext">The current HttpContext.</param>
    /// <param name="key">The key to associate with the stored data in the session.</param>
    /// <param name="data">The data to store in the session.</param>
    public static void Set<TData>(this HttpContext httpContext, string key, TData data)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNull(data);

        httpContext.Session.Set(key, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
    }

    /// <summary>
    /// Removes the data associated with the specified key from the current session.
    /// </summary>
    /// <param name="httpContext">The current HttpContext.</param>
    /// <param name="key">The key used to identify the data in the session.</param>
    public static void Remove(this HttpContext httpContext, string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        httpContext.Session.Remove(key);
    }
}

