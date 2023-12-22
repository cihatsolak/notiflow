namespace Puzzle.Lib.Auth.Infrastructure.Extensions;

/// <summary>
/// Contains extension methods for creating and managing JWT tokens.
/// </summary>
public static class JwtTokenExtensions
{
    /// <summary>
    /// Creates a refresh token with the specified size.
    /// </summary>
    /// <param name="size">The size of the refresh token to be created.</param>
    /// <returns>A string representation of the created refresh token.</returns>
    public static string CreateRefreshToken()
    {
        var number = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(number);

        return Convert.ToBase64String(number)
            .Replace("+", "-")
            .Replace("/", "_")
            .Replace("=", string.Empty);
    }
}