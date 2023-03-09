using BE.Core.Interfaces;
using BE.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BE.Authentication
{
    public class AuthenticationCustomize : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly IAccountServices _accountServices;
        public AuthenticationCustomize(
            IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAccountServices accountServices
            )
            : base(options, logger, encoder, clock)
        {
            _accountServices = accountServices;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(ConstantValue.AUTHORIZATION_KEY))
                return AuthenticateResult.NoResult();

            string authorizationHeader = Request.Headers[ConstantValue.AUTHORIZATION_KEY];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }
            var authentication = authorizationHeader.Split(ConstantValue.CHARACTER_EMPTY);
            if (!authentication[0].ToString().Equals(ConstantValue.BEARER_TYPE, StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail(ConstantValue.UNAUTHORIZED);
            }

            string token = authentication[1].Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail(ConstantValue.UNAUTHORIZED);
            }

            try
            {
                return await ValidateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private async Task<AuthenticateResult> ValidateToken(string token)
        {
            var result = await _accountServices.ValidateToken(token);
            if (string.IsNullOrEmpty(result.Data.Username) || !result.Data.isValid)
            {
                return AuthenticateResult.Fail(ConstantValue.UNAUTHORIZED);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Data.Username),
                new Claim("Id", result.Data.AccountId.ToString()),
                new Claim("UserId", result.Data.UserId.ToString()),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, new string[] { string.Empty });
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
