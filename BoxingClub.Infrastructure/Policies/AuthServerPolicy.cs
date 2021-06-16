using System;
using System.Net.Http;
using NLog;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;

namespace BoxingClub.Infrastructure.Policies
{
    public static class AuthServerPolicy
    {
        private const int DurationAfterFirstAttempt = 1;
        private const int DurationAfterSecondAttempt = 5;
        private const int Timeout = 10;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static AsyncRetryPolicy<HttpResponseMessage> GetWaitAndRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(DurationAfterFirstAttempt),
                    TimeSpan.FromSeconds(DurationAfterSecondAttempt)
                }, (exception, timeSpan, retryCount, context) =>
                {
                    _logger.Warn($"Retrying {retryCount} to connect AuthServer");
                });
        }

        public static AsyncTimeoutPolicy<HttpResponseMessage> GetTimeoutPolicy()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(Timeout);
        }
    }
}
