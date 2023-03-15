namespace Puzzle.Lib.Cookie.Infrastructure.Settings
{
    public sealed record CookieAuthenticationSetting 
    {
        public string LoginPath { get; init; }
        public string LogoutPath { get; init; }
        public string AccessDeniedPath { get; init; }
        public int ExpireHour { get; init; }
    }
}
