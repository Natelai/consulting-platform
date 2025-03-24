using Consulting.Auth.Application.Abstractions;
using Consulting.Auth.Application.Helpers;
using Consulting.Auth.Application.Models;
using Consulting.Auth.Contracts.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Consulting.Auth.Application;

public class AuthService(
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    IEmailService emailService,
    IOptions<JwtSettings> jwtSettings)
    : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly IEmailService _emailService = emailService;

    public async Task<LoginResponse> AuthenticateAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        
        if (user == null || !user.EmailConfirmed)
        {
            return new LoginResponse();
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (!result.Succeeded)
        {
            return new LoginResponse();
        }

        return new LoginResponse
        {
            IsSuccess = true,
            Token = GetSecurityToken(user)
        };
        
    }

    public async Task<bool> RegisterUserAsync(string email, string password)
    {
        var user = new IdentityUser 
        { 
            UserName = email, 
            Email = email, 
            EmailConfirmed = false, 
            LockoutEnabled = true 
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return false;
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink = $"https://localhost:7100/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";

        var emailBody = EmailTemplates.GetValidateEmailTemplate(email, confirmationLink);

        await _emailService
            .SendEmailAsync(email, "Підтвердження Email - Consulting Platform", emailBody);

        return true;
    }

    public async Task<bool> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null || user.EmailConfirmed)
        {
            return false;
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded;
    }

    public async Task<bool> ResendConfirmationEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null || user.EmailConfirmed)
        {
            return false;
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = $"https://localhost:7100/confirm-email?userId={user.Id}&token={WebUtility.UrlEncode(token)}";

        var emailBody = EmailTemplates.GetValidateEmailTemplate(email, confirmationLink);

        await _emailService
            .SendEmailAsync(email, "Повторне підтвердження Email - Consulting Platform", emailBody);

        return true;
    }

    private string GetSecurityToken(IdentityUser? user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id), new Claim(ClaimTypes.Email, user.Email!) }),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> SendPasswordResetEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return false;
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"https://localhost:5000/reset?userId={user.Id}&token={WebUtility.UrlEncode(token)}";

        var emailBody = EmailTemplates.GetResetPasswordTemplate(email, resetLink);

        await _emailService.SendEmailAsync(email, "Зміна пароля - Consulting Platform", emailBody);
        return true;
    }

    public async Task<bool> ResetPasswordAsync(string userId, string token, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        
        if (user == null)
        {
            return false;
        }

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
}
