using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Contracts.Requests;
using Consulting.Auth.Contracts.Responses;
using FastEndpoints;

namespace Consulting.Auth.Presentation.Endpoints
{
    public class ConfirmResetPasswordEndpoint(IAuthService authService)
                : Endpoint<ConfirmResetPasswordRequest, string>
    {
        private readonly IAuthService _authService = authService;

        public override void Configure()
        {
            Post("confirm-reset-password");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ConfirmResetPasswordRequest req, CancellationToken ct)
        {
            var success = await _authService.ResetPasswordAsync(req.UserId, req.Token, req.NewPassword);

            var redirectUrl = success
                ? "https://localhost:5000/email-confirmation-result?status=success"
                : "https://localhost:5000/email-confirmation-result?status=invalid";

            await SendAsync(redirectUrl, cancellation: ct);
        }
    }
}
