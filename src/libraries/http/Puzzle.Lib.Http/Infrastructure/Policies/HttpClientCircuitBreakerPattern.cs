﻿namespace Puzzle.Lib.Http.Infrastructure.Policies
{
    /// <summary>
    /// Extension methods to manage error status of http calls <see cref="HttpPolicyExtensions" />.
    /// </summary>
    public static class HttpClientCircuitBreakerPattern
    {
        /// <summary>
        /// Handles error states of http calls
        /// </summary>
        /// <remarks>Circuit Breaker Pattern | Basic</remarks>
        /// <param name="handledEventsAllowedBeforeBreaking">The number of exceptions or handled results that are allowed before opening the circuit.</param>
        /// <returns>type of async policy interface</returns>
        public static IAsyncPolicy<HttpResponseMessage> BasicCircuitBreakerPolicy(int handledEventsAllowedBeforeBreaking = 3)
        {
            return HttpPolicyExtensions
                  .HandleTransientHttpError()
                  .CircuitBreakerAsync(handledEventsAllowedBeforeBreaking, TimeSpan.FromSeconds(10), OnBreak, OnReset, OnHalfOpen);
        }

        // <summary>
        /// Handles error states of http calls
        /// </summary>
        /// <remarks>Circuit Breaker Pattern | Advanced</remarks>
        /// <param name="handledEventsAllowedBeforeBreaking">The number of exceptions or handled results that are allowed before opening the circuit.</param>
        /// <returns>type of async policy interface</returns>
        public static IAsyncPolicy<HttpResponseMessage> AdvancedCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                  .HandleTransientHttpError()
                  .AdvancedCircuitBreakerAsync(
                    failureThreshold: 0.25,
                    samplingDuration: TimeSpan.FromSeconds(60),
                    minimumThroughput: 7,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    OnBreak,
                    OnReset,
                    OnHalfOpen);
        }

        public static void OnBreak(DelegateResult<HttpResponseMessage> outcome, TimeSpan timeSpan)
        {
            Log.Error(outcome.Exception, "Http call terminated, requests will not flow.");
        }

        public static void OnReset()
        {
            Log.Information("Http call enabled, requests flow normally.");
        }

        public static void OnHalfOpen()
        {
            Log.Warning("Http call in test mode, one request will be allowed.");
        }
    }
}
