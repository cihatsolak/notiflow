﻿namespace Puzzle.Lib.Assistants.Constants
{
    /// <summary>
    /// A record that contains parameter names used in authentication.
    /// </summary>
    public record struct AuthenticationParameters
    {
        /// <summary>
        /// The parameter name for the access token.
        /// </summary>
        public const string AccessToken = "access_token";

        /// <summary>
        /// The parameter name for the refresh token.
        /// </summary>
        public const string RefreshToken = "refresh_token";

        /// <summary>
        /// The parameter name for the expiration time of the token.
        /// </summary>
        public const string ExpiresIn = "expires_in";
    }
}