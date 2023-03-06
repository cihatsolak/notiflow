namespace Puzzle.Lib.Http.Infrastructure.TransientFaultHandling
{
    public static class HttpClientRetryPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> AsyncRetryPolicy => HttpAsyncRetryPolicy();

        internal static IAsyncPolicy<HttpResponseMessage> HttpAsyncRetryPolicy()
        {
            return HttpPolicyExtensions
                  .HandleTransientHttpError()
                  .WaitAndRetryAsync(3, ComputeDuration, onRetry: (outcome, timeSpan, retryAttempt, context) =>
                  {
                      OnHttpRetry(outcome, timeSpan, retryAttempt, context);
                  });
        }

        private static void OnHttpRetry(DelegateResult<HttpResponseMessage> outcome, TimeSpan timeSpan, int retryAttempt, Context context)
        {
            Log.Fatal("{@RequestUri} adresine atılan istek {StatusCode} koduyla başarısız oldu. Bir sonraki denemeden önce {timeSpan} bekleniyor. Yeniden deneme girişimi: {retryCount}. Context : {@context}",
                outcome?.Result?.RequestMessage?.RequestUri, outcome?.Result?.StatusCode, timeSpan, retryAttempt, context);
        }

        private static TimeSpan ComputeDuration(int retryAttempt)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));
        }
    }
}
