namespace Puzzle.Lib.Database.Interceptors;

public sealed class SlowQueryInterceptor : DbCommandInterceptor
{
    public const int _slowQueryThreesholdInSeconds = 2;
    private readonly ILogger<SlowQueryInterceptor> _logger;

    public SlowQueryInterceptor(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<SlowQueryInterceptor>>();
    }

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        if (eventData.Duration.Seconds > _slowQueryThreesholdInSeconds)
        {
            _logger.LogWarning("Slow database query ({@Seconds} second) : {@CommandText}", eventData.Duration.Seconds, command.CommandText);
        }

        return base.ReaderExecuted(command, eventData, result);
    }
}
