﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TestWork.IService;

namespace TestWork
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        readonly IUserService _userService;

        public BasicAuthenticationHandler(
            IUserService userService,
            IOptionsMonitor<AuthenticationSchemeOptions>options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options,logger,encoder,clock)
        {
            _userService = userService;
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = "Basic";
            return base.HandleChallengeAsync(properties);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string username = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                username = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                if (!_userService.CheckUser(username,password))
                {
                    throw new ArgumentException("Неверный логин или пароль");
                }
            }

            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username)
            };

            var identity = new ClaimsIdentity(claims,Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return  AuthenticateResult.Success(ticket);
        }
    }
}
