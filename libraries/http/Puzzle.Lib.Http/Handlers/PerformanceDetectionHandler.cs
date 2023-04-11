namespace Puzzle.Lib.Http.Handlers
{
    /// <summary>
    /// A DelegatingHandler that measures the performance of HTTP requests and logs the elapsed time.
    /// </summary>
    public sealed class PerformanceDetectionHandler : DelegatingHandler
    {
        private readonly ILogger<PerformanceDetectionHandler> _logger;
        private readonly Stopwatch _stopwatch;

        public PerformanceDetectionHandler(ILogger<PerformanceDetectionHandler> logger, Stopwatch stopwatch)
        {
            _logger = logger;
            _stopwatch = stopwatch;
        }

        /// <summary>
        /// Sends an HTTP request to the inner handler and measures the elapsed time. Logs the elapsed time in seconds using the provided logger.
        /// </summary>
        /// <param name="request">The HTTP request message to send.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message sent by the inner handler.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Sending request to --> {@requestUri}", request.RequestUri);

            _stopwatch.Start();
            var httpResponseMessage = base.SendAsync(request, cancellationToken);
            _stopwatch.Stop();

            var elapsedSeconds = Math.Round(_stopwatch.ElapsedMilliseconds / 1000.0);
            _logger.LogInformation("Request completed in {elapsedSeconds} seconds.", elapsedSeconds);

            return httpResponseMessage;
        }
    }
}
