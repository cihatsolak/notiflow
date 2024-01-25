namespace Puzzle.Lib.Logging.Infrastructure;

internal static class ElasticsearchLoggerConfiguration
{
    internal static LoggerConfiguration WriteToElasticsearch(this LoggerConfiguration loggerConfiguration, SeriLogElasticSetting seriLogElasticSetting)
    {
        ElasticsearchSinkOptions elasticsearchSinkOptions = new(new Uri(seriLogElasticSetting.Address))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
            IndexFormat = $"{EnvironmentName}-{ApplicationName}-logs-{DateTime.Now:yyyy.MM.dd}",
            CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true, inlineFields: true),
            MinimumLogEventLevel = LogEventLevel.Information,
            FailureCallback = logEvent => Console.WriteLine($"Unable to submit event {logEvent.MessageTemplate}")
        };

        if (seriLogElasticSetting.IsRequiredAuthentication)
        {
            elasticsearchSinkOptions.ModifyConnectionSettings = (connection) => connection.BasicAuthentication(seriLogElasticSetting.Username, seriLogElasticSetting.Password);
        }

        return loggerConfiguration.WriteTo.Elasticsearch(elasticsearchSinkOptions);
    }

    private static string EnvironmentName 
    {
        get
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            }

            return environmentName.ToLowerInvariant();
        }
    }
    private static string ApplicationName => Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant();
}