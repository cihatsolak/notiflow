namespace Puzzle.Lib.WebStorage.Extensions;

/// <summary>
/// Defines methods for working with HTTP cookies.
/// </summary>
public static class CookieExtensions
{
    /// <summary>
    /// Retrieves the value of a cookie and deserializes it to the specified type.
    /// </summary>
    /// <typeparam name="TData">The type to deserialize the cookie value into.</typeparam>
    /// <param name="httpContext">The current HttpContext.</param>
    /// <param name="key">The key of the cookie to retrieve.</param>
    /// <returns>The deserialized value of the cookie, or the default value for the type if the cookie is not found or its value is empty.</returns>
    public static TData GetCookie<TData>(this HttpContext httpContext, string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        string cookieValue = httpContext.Request.Cookies[key];
        if (string.IsNullOrWhiteSpace(cookieValue))
            return default;

        return JsonSerializer.Deserialize<TData>(cookieValue);
    }

    /// <summary>
    /// Sets a cookie with the specified key, value, and expiration date.
    /// </summary>
    /// <typeparam name="TData">The type of data to store in the cookie.</typeparam>
    /// <param name="httpContext">The current HttpContext.</param>
    /// <param name="key">The key of the cookie.</param>
    /// <param name="value">The data to store in the cookie.</param>
    /// <param name="expireDate">The expiration date for the cookie.</param>
    public static void SetCookie<TData>(this HttpContext httpContext, string key, TData value, DateTime expireDate)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNull(value);

        if (DateTime.Now >= expireDate)
        {
            throw new ArgumentException("The expiration date cannot be less than today.");
        }

        httpContext.Response.Cookies.Append(key, JsonSerializer.Serialize(value), new CookieOptions { Expires = expireDate });
    }

    /// <summary>
    /// Sets a cookie with the specified key, value, and additional options.
    /// </summary>
    /// <typeparam name="TData">The type of data to store in the cookie.</typeparam>
    /// <param name="httpContext">The current HttpContext.</param>
    /// <param name="key">The key of the cookie.</param>
    /// <param name="value">The data to store in the cookie.</param>
    /// <param name="cookieOptions">Additional options for the cookie, including the expiration date.</param>
    public static void SetCookie<TData>(this HttpContext httpContext, string key, TData value, CookieOptions cookieOptions)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNull(value);

        if (DateTime.Now >= cookieOptions?.Expires)
        {
            throw new ArgumentException("The expiration date cannot be less than today.");
        }

        httpContext.Response.Cookies.Append(key, JsonSerializer.Serialize(value), cookieOptions);
    }

    /// <summary>
    /// Removes a cookie with the specified key.
    /// </summary>
    /// <param name="httpContext">The current HttpContext.</param>
    /// <param name="key">The key of the cookie to remove.</param>
    public static void RemoveCookie(this HttpContext httpContext, string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        httpContext.Response.Cookies.Delete(key);
    }

}
