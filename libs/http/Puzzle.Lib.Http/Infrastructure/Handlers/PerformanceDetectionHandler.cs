namespace Puzzle.Lib.Http.Infrastructure.Handlers;

/// <summary>
/// A DelegatingHandler that measures the performance of HTTP requests and logs the elapsed time.
/// </summary>
public sealed class PerformanceDetectionHandler(
    ILogger<PerformanceDetectionHandler> logger, 
    Stopwatch stopwatch) : DelegatingHandler
{
    /// <summary>
    /// Sends an HTTP request to the inner handler and measures the elapsed time. Logs the elapsed time in seconds using the provided logger.
    /// </summary>
    /// <param name="request">The HTTP request message to send.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the HTTP response message sent by the inner handler.</returns>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Sending request to --> {@requestUri}", request.RequestUri);

        stopwatch.Start();
        var httpResponseMessage = base.SendAsync(request, cancellationToken);
        stopwatch.Stop();

        var elapsedSeconds = Math.Round(stopwatch.ElapsedMilliseconds / 1000.0);
        logger.LogInformation("Request completed in {elapsedSeconds} seconds.", elapsedSeconds);

        stopwatch.Reset();

        return httpResponseMessage;
    }
}
