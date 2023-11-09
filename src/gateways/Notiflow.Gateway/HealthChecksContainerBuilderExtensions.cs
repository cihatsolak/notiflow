namespace Notiflow.Gateway;

internal static class HealthChecksContainerBuilderExtensions
{
    private static HealthCheckSetting healthCheckSetting;
    private const int MAXIMUM_NUMBER_OF_HISTORY_ENTRIES_TO_BE_STORED = 250;
    private const int MAXIMUM_NUMBER_OF_API_REQUESTS = 20;
    private const int TIMEOUT_PERIOD_SECOND = 60;

    internal static void AddConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        healthCheckSetting = builder.Configuration.GetRequiredSection(nameof(HealthCheckSetting)).Get<HealthCheckSetting>();

        builder.Services.AddHealthChecks();
        builder.Services.AddHealthChecksUI(settings =>
        {
            foreach (var endpoint in healthCheckSetting.EndpointControlSetting.OrEmptyIfNull())
            {
                settings.AddHealthCheckEndpoint(endpoint.Name, endpoint.Uri);
            }

            settings.SetEvaluationTimeInSeconds(TIMEOUT_PERIOD_SECOND); //Sağlık kontrolünün her bir öğesinin 60 saniyede bir değerlendirileceği bir zaman aşımı süresi belirler.
            settings.SetApiMaxActiveRequests(MAXIMUM_NUMBER_OF_API_REQUESTS); //Aynı anda işlenebilecek maksimum API isteği sayısını 50 olarak sınırlar.
            settings.MaximumHistoryEntriesPerEndpoint(MAXIMUM_NUMBER_OF_HISTORY_ENTRIES_TO_BE_STORED); //Her bir sağlık kontrolü sonucu için saklanacak maksimum geçmiş giriş sayısını 250 olarak ayarlar.

            settings.AddWebhookNotification(builder.Environment.ApplicationName,
             uri: healthCheckSetting.SlackWebHookSetting.Uri,
             payload: "{ text: \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
             restorePayload: "{ message: \"[[LIVENESS]] is back to life.\"}",
             shouldNotifyFunc: HandleShouldNotify,
             customMessageFunc: HandleCustomMessage,
             customDescriptionFunc: HandleCustomDescription);
        })
        .AddSqlServerStorage(healthCheckSetting.StorageConnectionString);
    }

    private static bool HandleShouldNotify(string livenessName, UIHealthReport report)
    {
        TimeSpan timeOfDay = DateTime.Now.TimeOfDay;
        return timeOfDay >= healthCheckSetting.SlackWebHookSetting.NotificationSendingStartTime && timeOfDay <= healthCheckSetting.SlackWebHookSetting.NotificationSendingEndTime;
    }

    private static string HandleCustomMessage(string livenessName, UIHealthReport report)
    {
        int failingCount = report.Entries.Count(entry => entry.Value.Status != UIHealthStatus.Healthy);
        return $"{failingCount} healthchecks are failing.";
    }

    private static string HandleCustomDescription(string livenessName, UIHealthReport report)
    {
        var failingMessages = report.Entries.Where(entry => entry.Value.Status != UIHealthStatus.Healthy).Select(ex => ex.Value.Exception);

        return string.Join("/", failingMessages);
    }
}
