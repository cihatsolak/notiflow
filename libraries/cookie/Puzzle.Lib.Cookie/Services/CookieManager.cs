namespace Puzzle.Lib.Cookie.Services
{
    internal sealed class CookieManager : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CookieOptions _cookieOptions;

        public CookieManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _cookieOptions = new()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Secure = true
            };
        }

        public TData Get<TData>(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[key];
            if (string.IsNullOrWhiteSpace(cookieValue))
                return default;

            return JsonSerializer.Deserialize<TData>(cookieValue);
        }

        public void Set<TData>(string key, TData value)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);
            ArgumentNullException.ThrowIfNull(value);

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, JsonSerializer.Serialize(value), _cookieOptions);
        }

        public void Set<TData>(string key, TData value, DateTime expireDate)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);
            ArgumentNullException.ThrowIfNull(value);

            _cookieOptions.Expires = expireDate;
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, JsonSerializer.Serialize(value), _cookieOptions);
        }

        public void Remove(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
    }
}
