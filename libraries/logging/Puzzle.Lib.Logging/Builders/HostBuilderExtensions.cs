namespace Puzzle.Lib.Logging.Builders
{
    public static class HostBuilderExtensions
    {
        public static void AddSeriLog(this WebApplicationBuilder webApplicationBuilder)
        {
            var logger = new LoggerConfiguration()
               .Filter.ByExcluding(Matching.FromSource("Microsoft"))
               .Filter.ByExcluding(Matching.FromSource("System"))
               .Enrich.FromLogContext()
               .Enrich.WithMachineName()
               .Enrich.WithEnvironmentUserName()
               .Enrich.WithProperty("ApplicationName", webApplicationBuilder.Environment.ApplicationName)
               .WriteTo.Debug(LogEventLevel.Verbose)
               .WriteTo.Console()
               .WriteToMsSqlServer("db connectionstring") //Todo:
               .WriteToMicrosoftTeams()
               .WriteToElasticsearch(webApplicationBuilder.Environment)
               .CreateLogger();

            webApplicationBuilder.Host.UseSerilog(logger);
        }

        public static async Task StartProjectAsync(this WebApplication app)
        {
            string applicationName = app.Environment.ApplicationName;
            var logger = Log.ForContext(typeof(HostBuilderExtensions));

            try
            {
                logger.Information("-- Starting web host: {@applicationName} --", applicationName);
                await app.RunAsync();
            }
            catch (Exception exception)
            {
                logger.Fatal(exception, "-- Host terminated unexpectedly. {@applicationName} -- ", applicationName);
                await app.StopAsync();
            }
            finally
            {
                Log.CloseAndFlush();
                await app.DisposeAsync();
            }
        }
    }
}