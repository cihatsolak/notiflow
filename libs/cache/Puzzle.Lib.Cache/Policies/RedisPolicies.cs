namespace Puzzle.Lib.Cache.Policies;

/// <summary>
/// This class provides a retry policy for handling errors while communicating with Redis. 
/// It contains a static internal class named "RedisRetryPolicies" that provides two properties, 
/// "AsyncRetryPolicy" and "RetryPolicy", which provide different retry policies for Redis.
/// </summary>
internal static class RedisPolicies
{
    /// <summary>
    /// Represents the maximum number of retry attempts allowed for a specific operation.
    /// </summary>
    private const int RETRY_COUNT = 2;

    /// <summary>
    /// Gets or sets the logger used for recording log information related to Redis retry policies.
    /// </summary>
    internal static ILogger Logger { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="action"></param>
    /// <returns></returns>
    internal static Task<TResult> ExecuteWithRetryAsync<TResult>(Func<Task<TResult>> action)
    {
        return Policy.Handle<Exception>().WaitAndRetryAsync(RETRY_COUNT, ComputeDuration, OnRedisRetry).ExecuteAsync(action);
    }

    /// <summary>
    /// Executes the specified action with retry and fallback policies.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the action.</typeparam>
    /// <param name="action">The action to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    internal static Task<TResult> ExecuteWithFallbackPolicy<TResult>(Func<Task<TResult>> action)
    {
        var fallbackPolicy = Policy<TResult>.Handle<Exception>().FallbackAsync(fallbackValue: default, OnFallbackAsync);
        var retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(RETRY_COUNT, ComputeDuration, OnRedisRetry);

        return retryPolicy.WrapAsync(fallbackPolicy).ExecuteAsync(action);
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
        Logger.LogError(exception, "An error occurred in redis communication. Waiting for {timeSpan} before next attempt. Retry attempt: {retryAttempt}. Context : {@context}", timeSpan, retryAttempt, context);
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

    /// <summary>
    /// Logs an error message when a Redis communication error occurs during a retry attempt.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="timeSpan">The time span to wait before the next retry attempt.</param>
    /// <param name="retryAttempt">The number of the retry attempt.</param>
    /// <param name="context">The context of the retry attempt.</param>
    private static Task OnFallbackAsync<T>(DelegateResult<T> delegateResult, Context context)
    {
        Logger.LogError(delegateResult.Exception, "Retry attempts failed for a key. Executing fallback.");

        return Task.CompletedTask;
    }
}
