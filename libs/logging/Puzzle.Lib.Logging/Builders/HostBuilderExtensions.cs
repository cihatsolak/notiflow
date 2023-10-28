using Microsoft.Extensions.Logging;

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
           .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
           .MinimumLevel.Override("System", LogEventLevel.Information)
           .Filter.ByExcluding(Matching.FromSource("Microsoft"))
           .Filter.ByExcluding(Matching.FromSource("System"))
           .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
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

        builder.Logging.ClearProviders();

        return builder.Host.UseSerilog(logger);
    }
}

