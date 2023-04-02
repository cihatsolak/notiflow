namespace Puzzle.Lib.Cache.Infrastructure.Settings
{
    /// <summary>
    /// Represents the configuration settings for the Redis server.
    /// </summary>
    public sealed record RedisServerSetting
    {
        /// <summary>
        /// Gets or sets the Redis server connection string.
        /// </summary>
        public string ConnectionString { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether to abort the connection if the server fails to respond during the connection phase.
        /// </summary>
        public bool AbortOnConnectFail { get; init; }

        /// <summary>
        /// Gets or sets the async timeout in milliseconds.
        /// </summary>
        public int AsyncTimeOutMilliSecond { get; init; }

        /// <summary>
        /// Gets or sets the connection timeout in milliseconds.
        /// </summary>
        public int ConnectTimeOutMilliSecond { get; init; }

        /// <summary>
        /// Gets or sets the Redis server username.
        /// </summary>
        public string Username { get; init; }

        /// <summary>
        /// Gets or sets the Redis server password.
        /// </summary>
        public string Password { get; init; }

        /// <summary>
        /// Gets or sets the default database to be used in the Redis server.
        /// </summary>
        public int DefaultDatabase { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether the Redis server allows administrative operations.
        /// </summary>
        public bool AllowAdmin { get; init; }
    }

}