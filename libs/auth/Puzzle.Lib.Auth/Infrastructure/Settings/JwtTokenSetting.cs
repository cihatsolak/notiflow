﻿namespace Puzzle.Lib.Auth.Infrastructure.Settings
{
    /// <summary>
    /// Represents a record that contains the settings related to JWT token authentication.
    /// </summary>
    public sealed record JwtTokenSetting
    {
        /// <summary>
        /// Gets or sets the list of valid audiences for the JWT token.
        /// </summary>
        public required IEnumerable<string> Audiences { get; init; }

        /// <summary>
        /// Gets or sets the issuer of the JWT token.
        /// </summary>
        [JsonRequired]
        public required string Issuer { get; init; }

        /// <summary>
        /// Gets or sets the expiration time of the access token in minutes.
        /// </summary>
        public required int AccessTokenExpirationMinute { get; init; }

        /// <summary>
        /// Gets or sets the expiration time of the refresh token in minutes.
        /// </summary>
        public required int RefreshTokenExpirationMinute { get; init; }

        /// <summary>
        /// Gets or sets the security key for JWT token authentication.
        /// </summary>
        [JsonRequired]
        public required string SecurityKey { get; init; }
    }
}