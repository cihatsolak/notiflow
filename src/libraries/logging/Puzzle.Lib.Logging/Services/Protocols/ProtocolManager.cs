namespace Puzzle.Lib.Logging.Services.Protocols
{
    public sealed class ProtocolManager : IProtocolService
    {
        private readonly HostingSetting _hostingSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ProtocolManager> _logger; //Todo

        public ProtocolManager(
            IOptions<HostingSetting> hostingSetting,
            IHttpContextAccessor httpContextAccessor,
            ILogger<ProtocolManager> logger)
        {
            _hostingSettings = hostingSetting.Value;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string IpAddress => GetCurrentIpAddress();

        /// <summary>
        /// Get IP address from HTTP context
        /// </summary>
        /// <returns>String of IP address</returns>
        private string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable())
                return string.Empty;

            string ipAddress = string.Empty;

            try
            {
                if (_httpContextAccessor.HttpContext.Request.Headers is not null)
                {
                    var forwardedHttpHeaderKey = "X-FORWARDED-FOR";
                    if (!string.IsNullOrEmpty(_hostingSettings.ForwardedHttpHeader))
                    {
                        forwardedHttpHeaderKey = _hostingSettings.ForwardedHttpHeader;
                    }

                    var forwardedHeader = _httpContextAccessor.HttpContext.Request.Headers[forwardedHttpHeaderKey];
                    if (!StringValues.IsNullOrEmpty(forwardedHeader))
                        ipAddress = forwardedHeader.FirstOrDefault();
                }

                if (string.IsNullOrEmpty(ipAddress) && _httpContextAccessor.HttpContext.Connection.RemoteIpAddress != null)
                    ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "The ip address of the request could not be detected.");
                return string.Empty;
            }

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
                ipAddress = ipAddress.Split(':').FirstOrDefault();
            }

            return ipAddress;
        }

        /// <summary>
        /// Check whether current HTTP request is available
        /// </summary>
        /// <returns>True if available; otherwise false</returns>
        private bool IsRequestAvailable()
        {
            if (_httpContextAccessor?.HttpContext is null)
                return false;

            try
            {
                if (_httpContextAccessor.HttpContext.Request is null)
                    return false;
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Request request not found. HttpContext: {@HttpContext}", _httpContextAccessor.HttpContext);
                return false;
            }

            return true;
        }
    }
}
