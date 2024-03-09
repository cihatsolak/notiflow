namespace Puzzle.Lib.Auth.Services;

/// <summary>
/// Defines properties for retrieving various claims related to a user's identity.
/// </summary>
public interface IClaimService
{
    /// <summary>
    /// Gets the email address claim value of the user.
    /// </summary>
    string Email { get; }

    /// <summary>
    /// Gets the username claim value of the user.
    /// </summary>
    string Username { get; }

    /// <summary>
    /// Gets the name claim value of the user.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the family name claim value of the user.
    /// </summary>
    string Surname { get; }

    /// <summary>
    /// Gets the name identifier claim value of the user.
    /// </summary>
    int NameIdentifier { get; }

    /// <summary>
    /// Gets the role claim value of the user.
    /// </summary>
    string Role { get; }

    /// <summary>
    /// Gets a list of role claim values of the user.
    /// </summary>
    IEnumerable<string> Roles { get; }

    /// <summary>
    /// Gets the JTI (JWT ID) claim value of the user.
    /// </summary>
    string Jti { get; }

    /// <summary>
    /// Gets the audience claim value of the user.
    /// </summary>
    string Audience { get; }

    /// <summary>
    /// Gets a list of audience claim values of the user.
    /// </summary>
    IEnumerable<string> Audiences { get; }

    /// <summary>
    /// Gets the given name claim value of the user.
    /// </summary>
    string GivenName { get; }

    /// <summary>
    /// Gets the issued-at (IAT) claim value of the user.
    /// </summary>
    /// <exception cref="JwtClaimException">If there is no issued at value in the claims, it is thrown</exception>
    DateTime Iat { get; }

    /// <summary>
    /// Gets the birth date claim value of the user.
    /// </summary>
    /// <exception cref="JwtClaimException">If there is no birtdate value in the claims, it is thrown</exception>
    DateTime BirthDate { get; }

    string GroupSid { get; }
}
