﻿namespace Puzzle.Lib.Database.Infrastructure;

/// <summary>
/// Represents a SQL setting, containing a connection string and a flag indicating whether the query should be split.
/// </summary>
public sealed record SqlSetting
{
    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    [JsonRequired]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the query should be split.
    /// </summary>
    public bool IsSplitQuery { get; set; }

    /// <summary>
    /// Gets or sets the command timeout duration in seconds for SQL queries.
    /// </summary>
    public int CommandTimeoutSecond { get; set; }

    /// <summary>
    /// Gets or sets the array of interceptors for SQL queries.
    /// </summary>
    public IInterceptor[] Interceptors { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether the application is running in a production environment.
    /// </summary>
    public bool IsProductionEnvironment { get; set; }
}
