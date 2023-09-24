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
    public static void AddSeriLog(this WebApplicationBuilder builder)
    {
        var logger = new LoggerConfiguration()
           .Filter.ByExcluding(Matching.FromSource("Microsoft"))
           .Filter.ByExcluding(Matching.FromSource("System"))
           .Enrich.FromLogContext()
           .Enrich.WithMachineName()
           .Enrich.WithEnvironmentUserName()
           .Enrich.WithProperty(nameof(builder.Environment.ApplicationName), builder.Environment.ApplicationName)
           .Enrich.WithProperty(nameof(builder.Environment.EnvironmentName), builder.Environment.EnvironmentName)
           .WriteTo.Debug(LogEventLevel.Verbose)
           .WriteTo.Console()
           .WriteToMsSqlServer("db connectionstring")
           .WriteToMicrosoftTeams()
           .WriteToElasticsearch(builder.Environment)
           .CreateLogger();

        builder.Host.UseSerilog(logger);
    }
}