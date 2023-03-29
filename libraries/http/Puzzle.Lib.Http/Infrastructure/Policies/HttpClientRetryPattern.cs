namespace Puzzle.Lib.Http.Infrastructure.Policies
{
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

        private static void OnRetry(DelegateResult<HttpResponseMessage> httpResponseMossage, TimeSpan timeSpan, int retryAttempt, Context context)
        {
            Log.Error(httpResponseMossage.Exception, "Request to {@RequestUri} failed with code {@StatusCode}. Waiting for {@timeSpan} before next attempt. Retry attempt: {@retryAttempt}. Context: {@context}",
                httpResponseMossage?.Result?.RequestMessage?.RequestUri, httpResponseMossage?.Result?.StatusCode, timeSpan, retryAttempt, context);
        }

        private static TimeSpan ComputeDuration(int retryAttempt)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        }
    }
}
