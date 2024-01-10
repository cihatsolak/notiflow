﻿namespace Puzzle.Lib.Logging.Infrastructure.Enrichers;

/// <summary>
/// The <c>CustomEnrichersExtensions</c> class provides extension methods for enhancing logging configuration.
/// </summary>
internal static class CustomEnrichersExtensions
{
    /// <summary>
    /// Adds an "ApplicationName" property to log entries for improved logging configuration.
    /// </summary>
    /// <param name="enrich">The <see cref="LoggerEnrichmentConfiguration"/> instance to extend.</param>
    /// <param name="applicationName">The name of the application to include in log entries.</param>
    /// <returns>A <see cref="LoggerConfiguration"/> instance with the "ApplicationName" property added.</returns>
    internal static LoggerConfiguration WithApplicationName(this LoggerEnrichmentConfiguration enrich)
    {
        ArgumentNullException.ThrowIfNull(enrich);
        return enrich.WithProperty("ApplicationName", Assembly.GetCallingAssembly().GetName().Name);
    }
}
