using Consulting.Auth.Contracts.Responses;

namespace Consulting.Auth.Application.Abstractions;

public interface IAuthService
{
    Task<LoginResponse> AuthenticateAsync(string email, string password);

    Task<bool> RegisterUserAsync(string email, string password);

    Task<bool> ConfirmEmailAsync(string userId, string token);

    Task<bool> ResendConfirmationEmailAsync(string email);

    Task<bool> SendPasswordResetEmailAsync(string email);

    Task<bool> ResetPasswordAsync(string userId, string token, string newPassword);
}
