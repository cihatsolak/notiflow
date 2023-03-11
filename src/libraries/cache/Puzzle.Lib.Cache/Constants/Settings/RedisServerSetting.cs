namespace Puzzle.Lib.Cache.Constants.Settings
{
    internal sealed record RedisServerSetting
    {
        public string ConnectionString { get; init; }
        public bool AbortOnConnectFail { get; init; }
        public int AsyncTimeOutMilliSecond { get; init; }
        public int ConnectTimeOutMilliSecond { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
        public int DefaultDatabase { get; init; }
        public bool AllowAdmin { get; init; }
    }
}