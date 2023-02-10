﻿namespace Notiflow.Lib.Cookie.Services
{
    public sealed class CookieManager : ICookieService
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

        public TModel Get<TModel>(string key)
        {
            string cookieValue = _httpContextAccessor.HttpContext.Request.Cookies[key];
            if (string.IsNullOrWhiteSpace(cookieValue))
                return default;

            return JsonSerializer.Deserialize<TModel>(cookieValue);
        }

        public void Set<TModel>(string key, TModel value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), ExceptionMessage.CookieKeyRequired);

            ArgumentNullException.ThrowIfNull(value);

            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, JsonSerializer.Serialize(value), _cookieOptions);
        }

        public void Set<TModel>(string key, TModel value, DateTime expireDate)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), ExceptionMessage.CookieKeyRequired);

            ArgumentNullException.ThrowIfNull(value);
            ArgumentNullException.ThrowIfNull(expireDate);

            _cookieOptions.Expires = expireDate;
            _httpContextAccessor.HttpContext.Response.Cookies.Append(key, JsonSerializer.Serialize(value), _cookieOptions);
        }

        public void Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), ExceptionMessage.CookieKeyRequired);

            _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
        }
    }
}
