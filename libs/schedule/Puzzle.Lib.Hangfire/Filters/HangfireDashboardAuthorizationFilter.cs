namespace Puzzle.Lib.Hangfire.Filters;

/// <summary>
/// Hangfire dashboard authorization filter implementation for checking whether the user is authenticated or not.
/// </summary>
public sealed class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    /// <summary>
    /// Determines whether the current user is authenticated or not.
    /// </summary>
    /// <param name="context">Dashboard context for the current request.</param>
    /// <returns>True if the user is authenticated; otherwise, false.</returns>
    public bool Authorize([NotNull] DashboardContext context)
    {
        HttpContext httpContext = context.GetHttpContext();
        return httpContext.User.Identity.IsAuthenticated;
    }
}
