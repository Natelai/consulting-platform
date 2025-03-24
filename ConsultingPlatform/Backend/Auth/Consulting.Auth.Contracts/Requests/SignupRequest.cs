namespace Consulting.Auth.Contracts.Requests;

public class SignupRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
