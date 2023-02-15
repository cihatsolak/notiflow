namespace Notiflow.Lib.Database.Infrastructure.Exceptions
{
    [Serializable]
    public sealed class DatabaseOperationException : Exception
    {
        public DatabaseOperationException()
        {
        }

        public DatabaseOperationException(string message) : base(message)
        {
        }

        private DatabaseOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DatabaseOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
