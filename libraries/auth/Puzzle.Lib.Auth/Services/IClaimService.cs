namespace Puzzle.Lib.Auth.Services
{
    /// <summary>
    /// Defines properties for retrieving various claims related to a user's identity.
    /// </summary>
    public interface IClaimService
    {
        /// <summary>
        /// Gets the email address claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no email value in the claims, it is thrown</exception>
        string Email { get; }

        /// <summary>
        /// Gets the name claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no name value in the claims, it is thrown</exception>
        string Name { get; }

        /// <summary>
        /// Gets the family name claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no family name value in the claims, it is thrown</exception>
        string FamilyName { get; }

        /// <summary>
        /// Gets the name identifier claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no name identifier value in claims or less than one, it is thrown.</exception>
        int NameIdentifier { get; }

        /// <summary>
        /// Gets the role claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no role value in the claims, it is thrown</exception>
        string Role { get; }

        /// <summary>
        /// Gets a list of role claim values of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no roles value in the claims, it is thrown</exception>
        IEnumerable<string> Roles { get; }

        /// <summary>
        /// Gets the JTI (JWT ID) claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no jti value in the claims, it is thrown</exception>
        string Jti { get; }

        /// <summary>
        /// Gets the audience claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no audience value in the claims, it is thrown</exception>
        string Audience { get; }

        /// <summary>
        /// Gets a list of audience claim values of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no audiences value in the claims, it is thrown</exception>
        IEnumerable<string> Audiences { get; }

        /// <summary>
        /// Gets the given name claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no given name value in the claims, it is thrown</exception>
        string GivenName { get; }

        /// <summary>
        /// Gets the issued-at (IAT) claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no issued at value in the claims, it is thrown</exception>
        DateTime Iat { get; }

        /// <summary>
        /// Gets the birth date claim value of the user.
        /// </summary>
        /// <exception cref="ClaimException">If there is no birtdate value in the claims, it is thrown</exception>
        DateTime BirthDate { get; }
    }
}
