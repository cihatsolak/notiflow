namespace Puzzle.Lib.Security.Services.Protocols
{
    public sealed class ProtocolManager : IProtocolService
    {
        private readonly HostingSetting _hostingSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProtocolManager(
            IOptions<HostingSetting> hostingSetting,
            IHttpContextAccessor httpContextAccessor)
        {
            _hostingSettings = hostingSetting.Value;
            _httpContextAccessor = httpContextAccessor;
        }

        public string IpAddress => GetCurrentIpAddress();

        /// <summary>
        /// Get IP address from HTTP context
        /// </summary>
        /// <returns>String of IP address</returns>
        private string GetCurrentIpAddress()
        {
            if (_httpContextAccessor.HttpContext?.Request is null)
                return string.Empty;

            string ipAddress = string.Empty;
            string forwardedHttpHeaderKey = "X-FORWARDED-FOR";

            if (_httpContextAccessor.HttpContext.Request.Headers is not null)
            {
                if (!string.IsNullOrEmpty(_hostingSettings.ForwardedHttpHeader))
                {
                    forwardedHttpHeaderKey = _hostingSettings.ForwardedHttpHeader;
                }

                string forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                if (!string.IsNullOrEmpty(forwardedHeader))
                {
                    ipAddress = forwardedHeader;
                }
            }

            if (string.IsNullOrEmpty(ipAddress) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress is not null)
                ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (ipAddress is not null && ipAddress.Equals(IPAddress.IPv6Loopback.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                ipAddress = IPAddress.Loopback.ToString();
            }

            if (IPAddress.TryParse(ipAddress ?? string.Empty, out var ip))
            {
                ipAddress = ip.ToString();
            }
            else if (!string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = ipAddress.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).FirstOrDefault();
            }

            return ipAddress;
        }
    }
}