﻿namespace Notiflow.IdentityServer.Core.Models;

public sealed record CreateAccessTokenRequest
{
    public string Username { get; init; }
    public string Password { get; init; }
}

public  sealed class CreateAccessTokenRequestValidator
{
    public CreateAccessTokenRequestValidator()
    {
        
    }
}