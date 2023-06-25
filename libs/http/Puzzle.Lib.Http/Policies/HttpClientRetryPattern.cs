namespace Puzzle.Lib.Http.Policies;

/// <summary>
/// Extension methods to manage error status of http calls <see cref="HttpPolicyExtensions" />.
/// </summary>
public static class HttpClientRetryPattern
{
    /// <summary>
    /// Handles error states of http calls
    /// </summary>
    /// <remarks>Retry Pattern</remarks>
    /// <param name="retryCount">total number of attempts. If no value is specified, it is treated as three.</param>
    /// <returns>type of async policy interface</returns>
    public static IAsyncPolicy<HttpResponseMessage> RetryPolicy(int retryCount = 3)
    {
        return HttpPolicyExtensions
              .HandleTransientHttpError()
              .WaitAndRetryAsync(retryCount, ComputeDuration, OnRetry);
    }

    /// <summary>
    /// Handles the retry logic when an HTTP request fails with an exception.
    /// </summary>
    /// <param name="httpResponseMessage">The response message of the HTTP request.</param>
    /// <param name="timeSpan">The time to wait before the next retry attempt.</param>
    /// <param name="retryAttempt">The number of the current retry attempt.</param>
    /// <param name="context">The context of the retry operation.</param>
    /// <remarks>
    /// This method logs an error message that includes information about the failed request, the HTTP status code,
    /// the time to wait before the next retry attempt, the current retry attempt number, and the retry context.
    /// </remarks>
    private static void OnRetry(DelegateResult<HttpResponseMessage> httpResponseMessage, TimeSpan timeSpan, int retryAttempt, Context context)
    {
        Log.Error(httpResponseMessage.Exception, "Request to {@RequestUri} failed with code {@StatusCode}. Waiting for {@timeSpan} before next attempt. Retry attempt: {@retryAttempt}. Context: {@context}",
            httpResponseMessage?.Result?.RequestMessage?.RequestUri, httpResponseMessage?.Result?.StatusCode, timeSpan, retryAttempt, context);
    }

    /// <summary>
    /// Computes the duration to wait before the next retry attempt based on the current retry attempt number.
    /// </summary>
    /// <param name="retryAttempt">The number of the current retry attempt.</param>
    /// <returns>The duration to wait before the next retry attempt.</returns>
    private static TimeSpan ComputeDuration(int retryAttempt)
    {
        return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
    }
}
