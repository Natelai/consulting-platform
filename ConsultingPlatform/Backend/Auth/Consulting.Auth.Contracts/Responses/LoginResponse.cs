﻿namespace Consulting.Auth.Contracts.Responses;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    public string? Token { get; set; }
}
