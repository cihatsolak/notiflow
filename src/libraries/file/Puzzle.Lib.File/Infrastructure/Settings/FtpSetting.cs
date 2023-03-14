namespace Puzzle.Lib.File.Infrastructure.Settings
{
    public sealed record FtpSetting
    {
        public string Ip { get; init; }
        public string Port { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public string Url { get; init; }
        public string Domain { get; init; }
    }
}
