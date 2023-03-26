namespace Puzzle.Lib.SeriLog.LoggerConfigurations
{
    internal static class MicrosoftTeamsLoggerConfiguration
    {
        internal static LoggerConfiguration WriteToMicrosoftTeams(this LoggerConfiguration loggerConfiguration)
        {
            MicrosoftTeamsSinkOptions microsoftTeamsSinkOptions =
                new("Hook!",
                    "A system error has occurred.");

            loggerConfiguration.WriteTo.MicrosoftTeams(
                microsoftTeamsSinkOptions: microsoftTeamsSinkOptions,
                restrictedToMinimumLevel: LogEventLevel.Error
                );

            return loggerConfiguration;
        }
    }
}
