﻿using DotnetWorkshop.Core.Services;
using DotnetWorkshop.Service.Authorization.Abstract;

namespace DotnetWorkshop.API.MiddleWares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtAuthenticationManager iJwtAuthenticationManager)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            string token = null;

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                var parts = authorizationHeader.Split(" ");
                if (parts.Length > 1)
                {
                    token = parts[parts.Length - 1];
                }
            }

            var userId = iJwtAuthenticationManager.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetByIdAsync(userId.Value).Result;
            }

            await _next(context);
        }
    }
}
