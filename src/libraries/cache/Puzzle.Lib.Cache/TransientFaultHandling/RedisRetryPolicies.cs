namespace Puzzle.Lib.Cache.TransientFaultHandling
{
    internal static class RedisRetryPolicies
    {
        private static ILogger Logger => Log.ForContext(typeof(RedisRetryPolicies));

        internal static AsyncRetryPolicy AsyncRetryPolicy => RedisRetryAsyncRetryPolicy();
        internal static RetryPolicy RetryPolicy => RedisRetryRetryPolicy();

        private static AsyncRetryPolicy RedisRetryAsyncRetryPolicy()
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(2, ComputeDuration, onRetry: OnRedisRetry);
        }

        private static RetryPolicy RedisRetryRetryPolicy()
        {
            return Policy.Handle<Exception>().WaitAndRetry(2, ComputeDuration, onRetry: OnRedisRetry);
        }

        private static void OnRedisRetry(Exception exception, TimeSpan timeSpan, int retryAttempt, Context context)
        {
            Logger.Error(exception, "An error occurred in redis communication. Waiting for {@timeSpan} before next attempt. Retry attempt: {@retryAttempt}. Context : {@context}", timeSpan, retryAttempt, context);
        }

        private static TimeSpan ComputeDuration(int retryAttempt)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        }
    }
}
