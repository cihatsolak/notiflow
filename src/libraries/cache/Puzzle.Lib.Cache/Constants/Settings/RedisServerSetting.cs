namespace Puzzle.Lib.Cache.Constants.Settings
{
    public interface IRedisServerSetting
    {
        string ConnectionString { get; init; }
        bool AbortOnConnectFail { get; init; }
        int AsyncTimeOutMilliSecond { get; init; }
        int ConnectTimeOutMilliSecond { get; init; }
        string Username { get; init; }
        string Password { get; init; }
        int DefaultDatabase { get; init; }
        bool AllowAdmin { get; init; }
    }

    internal sealed record RedisServerSetting : IRedisServerSetting
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