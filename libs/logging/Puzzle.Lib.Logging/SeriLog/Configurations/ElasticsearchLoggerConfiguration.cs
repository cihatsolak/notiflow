namespace Puzzle.Lib.Logging.SeriLog.Configurations;

internal static class ElasticsearchLoggerConfiguration
{
    internal static LoggerConfiguration WriteToElasticsearch(this LoggerConfiguration loggerConfiguration, IHostEnvironment hostEnvironment, SeriLogElasticSetting seriLogElasticSetting)
    {
        string applicationName = hostEnvironment.ApplicationName.Replace(".", "-").ToLowerInvariant();
        string environmentName = hostEnvironment.EnvironmentName.Replace(".", "-").ToLowerInvariant();

        ElasticsearchSinkOptions elasticsearchSinkOptions = new(new Uri(seriLogElasticSetting.Address))
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
            IndexFormat = $"{environmentName}-{applicationName}-logs{DateTime.Now:yyyy.MM.dd}"
        };

        if (seriLogElasticSetting.IsRequiredAuthentication)
        {
            elasticsearchSinkOptions.ModifyConnectionSettings = (connection) => connection.BasicAuthentication(seriLogElasticSetting.Username, seriLogElasticSetting.Password);
        }

        return loggerConfiguration.WriteTo.Elasticsearch();
    }
}