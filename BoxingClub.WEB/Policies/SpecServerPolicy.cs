using Microsoft.Extensions.Logging;
using NLog;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BoxingClub.Web.Policies
{
    public static class SpecServerPolicy
    {
        private const int DurationAfterFirstAttempt = 1;
        private const int DurationAfterSecondAttempt = 5;
        private const int DurationAfterThirdAttempt = 10;
        private const int Timeout = 5;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static AsyncRetryPolicy<HttpResponseMessage> GetWaitAndRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(DurationAfterFirstAttempt),
                    TimeSpan.FromSeconds(DurationAfterSecondAttempt),
                    TimeSpan.FromSeconds(DurationAfterThirdAttempt)
                }, (exception, timeSpan, retryCount, context) =>
                {
                    _logger.Warn("Retrying" + retryCount);
                });
        }

        public static AsyncTimeoutPolicy<HttpResponseMessage> GetTimeoutPolicy()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(Timeout);
        }
    }
}
