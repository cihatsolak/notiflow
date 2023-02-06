namespace Notiflow.Lib.Documentation.Infrastructure.Exceptions
{
    [Serializable]
    public sealed class SwaggerException : Exception
    {
        public SwaggerException()
        {
        }

        public SwaggerException(string message) : base(message)
        {
        }

        private SwaggerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SwaggerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}