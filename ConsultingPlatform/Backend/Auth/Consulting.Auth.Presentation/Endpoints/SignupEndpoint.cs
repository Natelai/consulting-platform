using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Contracts.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Identity.Data;

namespace Consulting.Auth.Presentation.Endpoints;

public class SignupEndpoint(IAuthService authService) 
    : Endpoint<RegisterRequest, RegisterResponse>
{
    private readonly IAuthService _authService = authService;

    public override void Configure()
    {
        Post("register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var success = await _authService.RegisterUserAsync(req.Email, req.Password);

        if (!success)
        {
            AddError("Registration failed. Email may already be in use.");
        }

        ThrowIfAnyErrors();
        await SendAsync(new RegisterResponse 
        {
            Message = "Registration successful. " +
            "Please check your email to confirm your account." 
        }, 
        cancellation: ct);
    }
}

