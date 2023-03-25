﻿namespace Puzzle.Lib.Auth.Services
{
    /// <summary>
    /// Defines properties for retrieving various claims related to a user's identity.
    /// </summary>
    public interface IClaimService
    {
        /// <summary>
        /// Gets the email address claim value of the user.
        /// </summary>
        string EmailAddress { get; }

        /// <summary>
        /// Gets the name claim value of the user.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the surname claim value of the user.
        /// </summary>
        string Surname { get; }

        /// <summary>
        /// Gets the user id claim value of the user.
        /// </summary>
        int UserId { get; }

        /// <summary>
        /// Gets the role claim value of the user.
        /// </summary>
        string Role { get; }

        /// <summary>
        /// Gets a list of role claim values of the user.
        /// </summary>
        List<string> Roles { get; }

        /// <summary>
        /// Gets the JTI (JWT ID) claim value of the user.
        /// </summary>
        string Jti { get; }

        /// <summary>
        /// Gets a list of audience claim values of the user.
        /// </summary>
        List<string> Audiences { get; }

        /// <summary>
        /// Gets the audience claim value of the user.
        /// </summary>
        string Audience { get; }

        /// <summary>
        /// Gets the username claim value of the user.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Gets the issued-at (IAT) claim value of the user.
        /// </summary>
        DateTime Iat { get; }

        /// <summary>
        /// Gets the birth date claim value of the user.
        /// </summary>
        DateTime BirthDate { get; }
    }
}