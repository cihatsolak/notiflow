namespace Puzzle.Lib.File.Infrastructure;

/// <summary>
/// Represents the settings required to connect to an FTP server.
/// </summary>
public sealed record FtpSetting
{
    /// <summary>
    /// Gets or sets the IP address of the FTP server.
    /// </summary>
    [JsonRequired]
    public string Ip { get; set; }

    /// <summary>
    /// Gets or sets the port number of the FTP server.
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the username to authenticate with the FTP server.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password to authenticate with the FTP server.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the URL of the FTP server.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the domain of the FTP server.
    /// </summary>
    public string Domain { get; set; }
}
