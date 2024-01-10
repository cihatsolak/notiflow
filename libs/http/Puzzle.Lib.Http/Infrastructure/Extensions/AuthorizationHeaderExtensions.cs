﻿namespace Puzzle.Lib.Http.Infrastructure.Extensions;

/// <summary>
/// Extensions for HttpRequestMessage
/// </summary>
public static class AuthorizationHeaderExtensions
{
    // <summary>
    /// Constant representing the "Bearer" authentication schema commonly used in OAuth 2.0.
    /// This schema is often employed for token-based authentication.
    /// </summary>
    private const string BEARER_SCHEMA = "Bearer";

    /// <summary>
    /// Constant representing the "Basic" authentication schema, a simple authentication scheme
    /// that involves sending a base64-encoded combination of username and password.
    /// </summary>
    private const string BASIC_SCHEMA = "Basic";

    /// <summary>
    /// Sets a basic authentication header.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    public static void SetBasicAuthentication(this HttpClient client, string userName, string password)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(BASIC_SCHEMA, EncodeCredential(userName, password));
    }

    /// <summary>
    /// Sets a basic authentication header.
    /// </summary>
    /// <param name="request">The HTTP request message.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    public static void SetBasicAuthentication(this HttpRequestMessage request, string userName, string password)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue(BASIC_SCHEMA, EncodeCredential(userName, password));
    }

    /// <summary>
    /// Sets an authorization header with a given scheme and value.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="scheme">The scheme.</param>
    /// <param name="token">The token.</param>
    public static void SetToken(this HttpClient client, string scheme, string token)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
    }

    /// <summary>
    /// Sets an authorization header with a given scheme and value.
    /// </summary>
    /// <param name="request">The HTTP request message.</param>
    /// <param name="scheme">The scheme.</param>
    /// <param name="token">The token.</param>
    public static void SetToken(this HttpRequestMessage request, string scheme, string token)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue(scheme, token);
    }

    /// <summary>
    /// Sets an authorization header with a bearer token.
    /// </summary>
    /// <param name="request">The HTTP request message.</param>
    /// <param name="token">The token.</param>
    public static void SetBearerToken(this HttpRequestMessage request, string token)
    {
        request.SetToken(BEARER_SCHEMA, token);
    }

    /// <summary>
    /// Sets an authorization header with a bearer token.
    /// </summary>
    /// <param name="client">The client.</param>
    /// <param name="token">The token.</param>
    public static void SetBearerToken(this HttpClient client, string token)
    {
        client.SetToken(BEARER_SCHEMA, token);
    }

    private static string EncodeCredential(string userName, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userName);

        Encoding encoding = Encoding.UTF8;
        string credential = string.Format("{0}:{1}", userName, password ?? string.Empty);

        return Convert.ToBase64String(encoding.GetBytes(credential));
    }
}
