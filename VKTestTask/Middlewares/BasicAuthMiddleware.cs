﻿using System.Text;
using VKTestTask.Services.Auth;

namespace VKTestTask.Middlewares;

public class BasicAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAuthorizationService _authService;

    public BasicAuthMiddleware(RequestDelegate next, IAuthorizationService authService)
    {
        _next = next;
        _authService = authService;
    }

    public async Task Invoke(HttpContext context)
    {
        var authHeader = context.Request.Headers.Authorization.ToString();
        if (!authHeader.StartsWith("Basic "))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var bytesAuthData = Convert.FromBase64String(authHeader.Split(' ')[1]);
        var authData = Encoding.UTF8.GetString(bytesAuthData);

        if (!authData.Contains(':'))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        var splittedData = authData.Split(':');
        var (login, password) = (splittedData[0], splittedData[1]);

        if (!_authService.HasAccess(login, password))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        await _next.Invoke(context);
    }
}