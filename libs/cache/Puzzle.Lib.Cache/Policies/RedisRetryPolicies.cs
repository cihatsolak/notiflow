namespace Puzzle.Lib.Cache.Policies;

/// <summary>
/// This class provides a retry policy for handling errors while communicating with Redis. 
/// It contains a static internal class named "RedisRetryPolicies" that provides two properties, 
/// "AsyncRetryPolicy" and "RetryPolicy", which provide different retry policies for Redis.
/// </summary>
internal static class RedisRetryPolicies
{
    /// <summary>
    /// Gets or sets the logger used for recording log information related to Redis retry policies.
    /// </summary>
    internal static ILogger Logger { get; set; }

    /// <summary>
    /// Gets an asynchronous retry policy for Redis.
    /// </summary>
    internal static AsyncRetryPolicy AsyncRetryPolicy => RedisRetryAsyncPolicy();

    /// <summary>
    /// Gets a retry policy for Redis.
    /// </summary>
    internal static RetryPolicy RetryPolicy => RedisRetryPolicy();

    /// <summary>
    /// Returns an asynchronous retry policy for Redis.
    /// </summary>
    /// <returns>An asynchronous retry policy.</returns>
    private static AsyncRetryPolicy RedisRetryAsyncPolicy()
    {
        return Policy.Handle<Exception>().WaitAndRetryAsync(2, ComputeDuration, OnRedisRetry);
    }

    /// <summary>
    /// Returns a retry policy for Redis.
    /// </summary>
    /// <returns>A retry policy.</returns>
    private static RetryPolicy RedisRetryPolicy()
    {
        return Policy.Handle<Exception>().WaitAndRetry(2, ComputeDuration, OnRedisRetry);
    }

    /// <summary>
    /// Logs an error message when a Redis communication error occurs during a retry attempt.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="timeSpan">The time span to wait before the next retry attempt.</param>
    /// <param name="retryAttempt">The number of the retry attempt.</param>
    /// <param name="context">The context of the retry attempt.</param>
    private static void OnRedisRetry(Exception exception, TimeSpan timeSpan, int retryAttempt, Context context)
    {
        Logger.LogError(exception, "An error occurred in redis communication. Waiting for {@timeSpan} before next attempt. Retry attempt: {@retryAttempt}. Context : {@context}", timeSpan, retryAttempt, context);
    }

    /// <summary>
    /// Computes the duration of the wait time for the next retry attempt.
    /// </summary>
    /// <param name="retryAttempt">The number of the retry attempt.</param>
    /// <returns>The duration of the wait time.</returns>
    private static TimeSpan ComputeDuration(int retryAttempt)
    {
        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
    }
}
