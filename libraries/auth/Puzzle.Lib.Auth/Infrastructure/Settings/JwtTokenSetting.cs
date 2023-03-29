﻿namespace Puzzle.Lib.Auth.Infrastructure.Settings
{
    /// <summary>
    /// Represents a record that contains the settings related to JWT token authentication.
    /// </summary>
    internal sealed record JwtTokenSetting
    {
        /// <summary>
        /// Gets or sets the list of valid audiences for the JWT token.
        /// </summary>
        public IEnumerable<string> Audiences { get; init; }

        /// <summary>
        /// Gets or sets the issuer of the JWT token.
        /// </summary>
        public string Issuer { get; init; }

        /// <summary>
        /// Gets or sets the expiration time of the access token in minutes.
        /// </summary>
        public int AccessTokenExpiration { get; init; }

        /// <summary>
        /// Gets or sets the expiration time of the refresh token in minutes.
        /// </summary>
        public int RefreshTokenExpiration { get; init; }

        /// <summary>
        /// Gets or sets the security key for JWT token authentication.
        /// </summary>
        public string SecurityKey { get; init; }
    }
}
