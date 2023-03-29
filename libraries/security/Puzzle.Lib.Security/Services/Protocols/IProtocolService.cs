namespace Puzzle.Lib.Security.Services.Protocols
{
    public interface IProtocolService
    {
        /// <summary>
        /// Get IP address from HTTP context
        /// </summary>
        /// <returns>String of IP address</returns>
        string IpAddress { get; }
    }
}
