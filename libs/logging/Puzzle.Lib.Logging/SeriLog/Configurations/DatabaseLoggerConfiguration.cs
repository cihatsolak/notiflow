namespace Puzzle.Lib.Logging.SeriLogConfigurations.Configurations;

internal static class DatabaseLoggerConfiguration
{
    private const string LogTableName = "Logs";

    internal static LoggerConfiguration WriteToMsSqlServer(this LoggerConfiguration loggerConfiguration, string connectionString)
    {
        Serilog.Sinks.MSSqlServer.ColumnOptions columnOptions = new()
        {
            AdditionalColumns = new Collection<SqlColumn>
           {
              new SqlColumn("CorrelationId", SqlDbType.VarChar, allowNull: true, dataLength: 150),
              new SqlColumn("TraceIdentifier", SqlDbType.VarChar, allowNull: true, dataLength: 70)
           },
        };

        loggerConfiguration.WriteTo.MSSqlServer(
            connectionString: connectionString,
            sinkOptions: new MSSqlServerSinkOptions
            {
                TableName = LogTableName,
                AutoCreateSqlTable = true,
                SchemaName = "dbo"
            },
            sinkOptionsSection: null,
            appConfiguration: null,
            restrictedToMinimumLevel: LogEventLevel.Warning,
            formatProvider: null,
            columnOptions: columnOptions,
            columnOptionsSection: null,
            logEventFormatter: null);

        return loggerConfiguration;
    }

    internal static LoggerConfiguration WriteToPostgreServer(this LoggerConfiguration loggerConfiguration, string connectionString)
    {
        loggerConfiguration.WriteTo.PostgreSQL(connectionString,
        tableName: LogTableName,
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
            {"user_name", new UsernameColumnWriter()}
        },
        restrictedToMinimumLevel: LogEventLevel.Verbose
        );

        return loggerConfiguration;
    }
}
