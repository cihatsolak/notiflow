namespace Puzzle.Lib.Logging.SeriLogConfigurations.Configurations;

internal static class ElasticsearchLoggerConfiguration
{
    internal static LoggerConfiguration WriteToElasticsearch(this LoggerConfiguration loggerConfiguration, IWebHostEnvironment webHostEnvironment)
    {
        string applicationName = webHostEnvironment.ApplicationName.Replace(".", "-").ToLowerInvariant();
        string environmentName = webHostEnvironment.EnvironmentName.Replace(".", "-").ToLowerInvariant();

        loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("Elastic url eklenecek"))
        {
            CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,
            TemplateName = "serilog-events-template",
            TypeName = $"{applicationName}logevent",
            BatchPostingLimit = 50,
            Period = TimeSpan.FromSeconds(2),
            InlineFields = true,
            MinimumLogEventLevel = LogEventLevel.Information,
            BufferBaseFilename = "serilog-buffer",
            BufferFileSizeLimitBytes = 5242880,
            BufferLogShippingInterval = TimeSpan.FromSeconds(5),
            IndexFormat = $"applogs-{applicationName}-{environmentName}-log{DateTime.Now:yyyy.MM.dd}"
        });

        return loggerConfiguration;
    }
}
