using Consulting.Auth.Application;
using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Contracts.Requests;
using Consulting.Auth.Contracts.Responses;
using FastEndpoints;

namespace Consulting.Auth.Presentation.Endpoints;

public class ConfirmEmailEndpoint(IAuthService authService) 
    : EndpointWithoutRequest<ConfirmEmailResponse>
{
    private readonly IAuthService _authService = authService;

    public override void Configure()
    {
        Get("confirm-email");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Query<string>("userId");
        var token = Query<string>("token");

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            await SendRedirectAsync("https://localhost:5000/email-confirmation?status=invalid");
            return;
        }

        var success = await _authService.ConfirmEmailAsync(userId, token);
        var redirectUrl = success
            ? "https://localhost:5000/email-confirmation?status=success"
            : "https://localhost:5000/email-confirmation?status=failure";

        await SendRedirectAsync(redirectUrl, isPermanent: false, allowRemoteRedirects: true);
    }
}
