namespace Notiflow.Lib.Cache.Constants.Exceptions
{
    internal static class RedisArgumentException
    {
        internal static void ThrowIfNegativeNumber(int argument, string paramName = null)
        {
            if (0 > argument)
            {
                throw new ArgumentException(ExceptionMessage.NegativeNumber, paramName);
            }
        }
    }
}
