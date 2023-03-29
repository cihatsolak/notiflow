namespace Puzzle.Lib.Logging.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        public static IServiceCollection AddCustomHttpLogging(this IServiceCollection services)
        {
            services.AddHttpLogging(httpLoggingOptions =>
            {
                httpLoggingOptions.LoggingFields = HttpLoggingFields.All;
                httpLoggingOptions.RequestHeaders.Add("sec-ch-ua");
                httpLoggingOptions.MediaTypeOptions.AddText("application/javascript");
                httpLoggingOptions.RequestBodyLogLimit = 4096;
                httpLoggingOptions.ResponseBodyLogLimit = 4096;
            });

            return services;
        }
    }
}
