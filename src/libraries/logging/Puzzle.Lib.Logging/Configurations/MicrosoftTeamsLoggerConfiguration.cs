namespace Puzzle.Lib.SeriLog.LoggerConfigurations
{
    internal static class MicrosoftTeamsLoggerConfiguration
    {
        internal static LoggerConfiguration WriteToMicrosoftTeams(this LoggerConfiguration loggerConfiguration)
        {
            MicrosoftTeamsSinkOptions microsoftTeamsSinkOptions =
                new("https://dogusgrubu.webhook.office.com/webhookb2/1edb7160-0da7-43f6-8e85-cc9fec8918a6@cc76235c-86ab-4979-bc0b-0e78c66edb7c/IncomingWebhook/af8fe6f7834b4962b782149e0861eb42/8f0e1325-56da-461c-a59c-f181a9b60b7e",
                    "Sistemsel bir hata oluştu.");

            loggerConfiguration.WriteTo.MicrosoftTeams(
                microsoftTeamsSinkOptions: microsoftTeamsSinkOptions,
                restrictedToMinimumLevel: LogEventLevel.Error
                );

            return loggerConfiguration;
        }
    }
}
