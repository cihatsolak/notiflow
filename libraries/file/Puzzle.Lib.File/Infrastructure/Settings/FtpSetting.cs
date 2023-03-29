namespace Puzzle.Lib.File.Infrastructure.Settings
{
    /// <summary>
    /// Represents the settings required to connect to an FTP server.
    /// </summary>
    public sealed record FtpSetting
    {
        /// <summary>
        /// Gets or sets the IP address of the FTP server.
        /// </summary>
        public string Ip { get; init; }

        /// <summary>
        /// Gets or sets the port number of the FTP server.
        /// </summary>
        public string Port { get; init; }

        /// <summary>
        /// Gets or sets the username to authenticate with the FTP server.
        /// </summary>
        public string Username { get; init; }

        /// <summary>
        /// Gets or sets the password to authenticate with the FTP server.
        /// </summary>
        public string Password { get; init; }

        /// <summary>
        /// Gets or sets the URL of the FTP server.
        /// </summary>
        public string Url { get; init; }

        /// <summary>
        /// Gets or sets the domain of the FTP server.
        /// </summary>
        public string Domain { get; init; }
    }
}
