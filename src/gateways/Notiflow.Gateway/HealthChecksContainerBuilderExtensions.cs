namespace Notiflow.Gateway;

internal static class HealthChecksContainerBuilderExtensions
{
    private static HealthCheckSetting healthCheckSetting;

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
