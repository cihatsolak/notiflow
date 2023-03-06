namespace Puzzle.Lib.Cookie.Infrastructure.Settings
{
    public interface ICookieAuthenticationSetting
    {
        string LoginPath { get; init; }
        string LogoutPath { get; init; }
        string AccessDeniedPath { get; init; }
        int ExpireHour { get; init; }
    }

    internal sealed record CookieAuthenticationSetting : ICookieAuthenticationSetting
    {
        public string LoginPath { get; init; }
        public string LogoutPath { get; init; }
        public string AccessDeniedPath { get; init; }
        public int ExpireHour { get; init; }
    }
}
