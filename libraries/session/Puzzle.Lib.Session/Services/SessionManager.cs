namespace Puzzle.Lib.Session.Services
{
    public sealed class SessionManager : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetString(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            bool isExists = _httpContextAccessor.HttpContext.Session.TryGetValue(key, out byte[] value);
            if (!isExists)
                return default;

            return Encoding.UTF8.GetString(value);
        }

        public TData Get<TData>(string key) where TData : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            bool isExists = _httpContextAccessor.HttpContext.Session.TryGetValue(key, out byte[] value);
            if (!isExists)
                return default;

            return JsonSerializer.Deserialize<TData>(Encoding.UTF8.GetString(value));
        }

        public void SetString(string key, string value)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);
            ArgumentException.ThrowIfNullOrEmpty(value);

            _httpContextAccessor.HttpContext.Session.Set(key, Encoding.UTF8.GetBytes(value));
        }

        public void Set<TData>(string key, TData data) where TData : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(key);
            ArgumentNullException.ThrowIfNull(data);

            _httpContextAccessor.HttpContext.Session.Set(key, Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
        }

        public void Remove(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            _httpContextAccessor.HttpContext.Session.Remove(key);
        }
    }
}
