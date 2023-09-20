using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestProject.Authentication.Services;

namespace TestProject.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthChecker _authChecker;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthChecker authChecker) : base(options, logger, encoder, clock)
        {
            _authChecker = authChecker;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization header");
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();

            if (!authorizationHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Authorization header does not start with 'Basic'");
            }

            var authBase64Decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)));
            var authSplit = authBase64Decoded.Split(new[] { ':' }, 2);

            if (authSplit.Length != 2)
            {
                return AuthenticateResult.Fail("Invalid Authorization header format");
            }

            var username = authSplit[0];
            var password = authSplit[1];

            if (!await _authChecker.CheckAuthAsync(username, password, CancellationToken.None))
            {
                return AuthenticateResult.Fail($"The password is incorrect for the user '{username}'");
            }

            var client = new BasicAuthenticationClient
            {
                AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
                IsAuthenticated = true,
                Name = username
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new[]
            {
                new Claim(ClaimTypes.Name, username)
            }));

            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
		}
    }
}
