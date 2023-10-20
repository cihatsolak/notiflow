namespace Puzzle.Lib.Logging.Builders;

/// <summary>
/// Provides extension methods for configuring logging in a web application.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Adds Serilog logging to the web application.
    /// </summary>
    /// <param name="builder">The web application builder.</param>
    public static IHostBuilder AddSeriLogWithElasticSearch(this WebApplicationBuilder builder, Action<SeriLogElasticSetting> configure)
    {
        SeriLogElasticSetting seriLogElasticSetting = new();
        configure?.Invoke(seriLogElasticSetting);

        var logger = new LoggerConfiguration()
           .Filter.ByExcluding(Matching.FromSource("Microsoft"))
           .Filter.ByExcluding(Matching.FromSource("System"))
           .Enrich.FromLogContext()
           .Enrich.WithMachineName()
           .Enrich.WithEnvironmentUserName()
           .Enrich.WithEnvironmentName()
           .Enrich.WithExceptionDetails()
           .Enrich.WithCorrelationId()
           .Enrich.WithClientIp()
           .Enrich.WithThreadId()
           .Enrich.WithThreadName()
           .Enrich.WithApplicationName(builder.Environment.ApplicationName)
           .WriteTo.Debug(LogEventLevel.Verbose)
           .WriteTo.Console()
           .WriteToElasticsearch(builder.Environment, seriLogElasticSetting)
           .CreateLogger();

        return builder.Host.UseSerilog(logger);
    }
}

