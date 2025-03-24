using Consulting.Auth.Contracts.Requests;
using FastEndpoints;
using FluentValidation.Results;
using System.Collections.Concurrent;

namespace Consulting.Auth.Presentation.Endpoints.Preprocessors
{
    public class RequestRateLimiterPreprocessor : IPreProcessor<ResendConfirmationRequest>
    {
        private static readonly ConcurrentDictionary<string, DateTime> _lastRequestTime = new();

        public Task PreProcessAsync(IPreProcessorContext<ResendConfirmationRequest> context, CancellationToken ct)
        {
            var email = context?.Request?.Email;
            var now = DateTime.UtcNow;

            if (_lastRequestTime.TryGetValue(email, out var lastRequest) && (now - lastRequest).TotalSeconds < 60)
            {
                context?.ValidationFailures.Add(new ValidationFailure("Email", "Too many requests. Please wait before retrying."));
                return Task.CompletedTask;
            }

            _lastRequestTime[email] = now;
            return Task.CompletedTask;
        }
    }
}
