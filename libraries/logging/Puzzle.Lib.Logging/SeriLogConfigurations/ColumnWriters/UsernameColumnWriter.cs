namespace Puzzle.Lib.Logging.SeriLogConfigurations.ColumnWriters
{
    public class UsernameColumnWriter : ColumnWriterBase
    {
        public UsernameColumnWriter() : base(NpgsqlDbType.Varchar, 70)
        {
        }

        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            (_, LogEventPropertyValue value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
            return value?.ToString() ?? null;
        }
    }
}
