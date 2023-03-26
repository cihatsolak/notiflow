namespace Puzzle.Lib.SeriLog.LoggerConfigurations
{
    internal static class DatabaseLoggerConfiguration
    {
        private const string LogTableName = "Logs";

        internal static LoggerConfiguration WriteToMsSqlServer(this LoggerConfiguration loggerConfiguration, string connectionString)
        {
            Serilog.Sinks.MSSqlServer.ColumnOptions columnOptions = new()
            {
               AdditionalColumns = new Collection<SqlColumn>
               {
                  new SqlColumn(LogPushProperties.IpAddress, SqlDbType.VarChar, allowNull: true, dataLength: 100),
                  new SqlColumn(LogPushProperties.CorrelationId, SqlDbType.VarChar, allowNull: true, dataLength: 150),
                  new SqlColumn(LogPushProperties.TraceIdentifier, SqlDbType.VarChar, allowNull: true, dataLength: 70),
                  new SqlColumn(LogPushProperties.Device, SqlDbType.VarChar, allowNull: true, dataLength: 70),
                  new SqlColumn(LogPushProperties.OperatingSystem, SqlDbType.VarChar, allowNull: true, dataLength: 70),
                  new SqlColumn(LogPushProperties.Browser, SqlDbType.VarChar, allowNull: true, dataLength: 70),

                  new SqlColumn(LogPushProperties.ApplicationName, SqlDbType.VarChar, allowNull: true, dataLength: 70),
                  new SqlColumn(LogPushProperties.MachineName, SqlDbType.NVarChar, allowNull: true, dataLength: 70),

                  new SqlColumn(LogPushProperties.RequestMethod, SqlDbType.NVarChar, allowNull: true, dataLength: 70),
                  new SqlColumn(LogPushProperties.RequestPath, SqlDbType.NVarChar, allowNull: true, dataLength: 70),
                  new SqlColumn(LogPushProperties.SourceContext, SqlDbType.NVarChar, allowNull: true, dataLength: 70),
                  new SqlColumn(LogPushProperties.EnvironmentUserName, SqlDbType.NVarChar, allowNull: true, dataLength: 150)
               },
            };

            loggerConfiguration.WriteTo.MSSqlServer(
                connectionString: connectionString,
                sinkOptions: new MSSqlServerSinkOptions { TableName = LogTableName },
                sinkOptionsSection: null,
                appConfiguration: null,
                restrictedToMinimumLevel: LogEventLevel.Verbose,
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
}
