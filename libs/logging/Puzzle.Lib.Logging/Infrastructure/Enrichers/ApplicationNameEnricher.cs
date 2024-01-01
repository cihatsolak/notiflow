namespace Puzzle.Lib.Logging.Infrastructure.Enrichers;

public class ApplicationNameEnricher : ILogEventEnricher
{
    private const string APPLICATION_NAME_PROPERTY_NAME = "ApplicationName";

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(new LogEventProperty(APPLICATION_NAME_PROPERTY_NAME, new ScalarValue("")));
    }
}
