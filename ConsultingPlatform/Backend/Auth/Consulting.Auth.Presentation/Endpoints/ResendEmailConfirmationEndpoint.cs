using Consulting.Auth.Application;
using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Contracts.Requests;
using Consulting.Auth.Contracts.Responses;
using Consulting.Auth.Presentation.Endpoints.Preprocessors;
using FastEndpoints;

namespace Consulting.Auth.Presentation.Endpoints;

public class ResendEmailConfirmationEndpoint(IAuthService authService)
    : Endpoint<ResendConfirmationRequest, ResendConfirmationResponse>
{
    private readonly IAuthService _authService = authService;

    public override void Configure()
    {
        Post("resend-confirmation");
        AllowAnonymous();
        PreProcessor<RequestRateLimiterPreprocessor>();
    }

    public override async Task HandleAsync(ResendConfirmationRequest req, CancellationToken ct)
    {
        ThrowIfAnyErrors();

        var success = await _authService.ResendConfirmationEmailAsync(req.Email);

        if (!success)
        {
            AddError("Invalid email or already confirmed.");
            await SendErrorsAsync(400, ct);
            return;
        }

        await SendAsync(new ResendConfirmationResponse { Message = "Confirmation email sent successfully." }, cancellation: ct);
    }
}

