namespace Puzzle.Lib.Http.Infrastructure.Headers;

/// <summary>
/// HTTP Basic Authentication authorization header
/// </summary>
/// <seealso cref="AuthenticationHeaderValue" />
public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BasicAuthenticationHeaderValue"/> class.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    public BasicAuthenticationHeaderValue(string userName, string password)
        : base("Basic", EncodeCredential(userName, password))
    { 
    }

    /// <summary>
    /// Encodes the credential.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">userName</exception>
    public static string EncodeCredential(string userName, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        Encoding encoding = Encoding.UTF8;
        string credential = string.Format("{0}:{1}", userName, password ?? string.Empty);

        return Convert.ToBase64String(encoding.GetBytes(credential));
    }
}
