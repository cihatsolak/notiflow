namespace Puzzle.Lib.Database.Interceptors;

/// <summary>
/// Interceptor for tracking slow database queries and logging them.
/// </summary>
/// <remarks>
/// This class is used to intercept database commands and log those that exceed a specified time threshold as slow queries.
/// </remarks>
public sealed class SlowQueryInterceptor : DbCommandInterceptor
{
    public const int SLOW_QUERY_THREE_SHOLD_IN_SECONDS = 3;
    private readonly ILogger<SlowQueryInterceptor> _logger;

    public SlowQueryInterceptor(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<SlowQueryInterceptor>>();
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        if (eventData.Duration.Seconds > SLOW_QUERY_THREE_SHOLD_IN_SECONDS)
        {
            _logger.LogWarning($"Slow database query ({eventData.Duration.Seconds} second) : {command.CommandText}");
        }

        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }
}

