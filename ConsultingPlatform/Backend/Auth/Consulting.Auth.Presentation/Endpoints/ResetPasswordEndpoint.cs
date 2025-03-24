using Consulting.Auth.Application;
using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Contracts.Requests;
using Consulting.Auth.Contracts.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json.Linq;
using YamlDotNet.Core.Tokens;

namespace Consulting.Auth.Presentation.Endpoints
{
    public class ResetPasswordEndpoint(IAuthService authService) 
        : EndpointWithoutRequest<string>
    {
        private readonly IAuthService _authService = authService;

        public override void Configure()
        {
            Get("reset-password");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var email = Query<string>("email");

            var success = await _authService.SendPasswordResetEmailAsync(email);

            var redirectUrl = success
                ? $"https://localhost:5000/email-confirmation-password?status=waiting&email={email}"
                : $"https://localhost:5000/email-confirmation-password?status=invalid&email={email}";

            await SendAsync(redirectUrl);
        }
    }
}
