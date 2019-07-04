using Conjure.Security;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Example.Client.Data
{
    public class ClientAuthenticationStateProvider : AuthenticationStateProvider
    {
        private static readonly ClaimsIdentity AnonymousIdentity = new ClaimsIdentity();
        private readonly HttpClient _http;

        public ClientAuthenticationStateProvider(HttpClient http)
        {
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await _http.GetJsonAsync<AppUser>("api/security/user");
            var identity = user.IsAuthenticated
                ? new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, user.Name) },
                    AppUser.AuthenticationType)
                : AnonymousIdentity;

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}
