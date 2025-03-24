using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Contracts.Requests;
using Consulting.Auth.Contracts.Responses;
using FastEndpoints;

namespace Consulting.Auth.Presentation.Endpoints;

public class LoginEndpoint(IAuthService authService) 
    : Endpoint<LoginRequest, LoginResponse>
{
    private readonly IAuthService _authService = authService;

    public override void Configure()
    {
        Post("login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var authResult = await _authService.AuthenticateAsync(req.Email, req.Password);

        await SendAsync(authResult, cancellation: ct);
    }
}
