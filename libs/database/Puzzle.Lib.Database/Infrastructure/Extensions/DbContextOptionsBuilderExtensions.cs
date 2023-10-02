namespace Puzzle.Lib.Database.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring warnings and logging in a DbContextOptionsBuilder instance.
/// </summary>
internal static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Configures the given DbContextOptionsBuilder instance to ignore specific warnings.
    /// </summary>
    /// <param name="contextOptions">The DbContextOptionsBuilder instance to configure.</param>
    internal static void ConfigureCustomWarnings(this DbContextOptionsBuilder contextOptions)
    {
        contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
        contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning));
        contextOptions.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.BoolWithDefaultWarning));
        contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
    }

    /// <summary>
    /// Configures the given DbContextOptionsBuilder instance for logging.
    /// </summary>
    /// <param name="contextOptions">The DbContextOptionsBuilder instance to configure.</param>
    /// <param name="isProduction">A flag indicating whether the application is running in a production environment.</param>
    internal static void ConfigureCustomLogs(this DbContextOptionsBuilder contextOptions, bool isProduction)
    {
        contextOptions.EnableDetailedErrors(!isProduction);
        contextOptions.EnableSensitiveDataLogging(!isProduction);
        contextOptions.LogTo(Console.WriteLine, LogLevel.Information);
        contextOptions.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
    }
}
