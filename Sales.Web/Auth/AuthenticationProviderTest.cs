using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Sales.Web.Auth
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimus = new ClaimsIdentity();
            var jorge = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName","Juan"),
                new Claim("LastName", "Zulu"),
                new Claim(ClaimTypes.Name, "zulu@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },authenticationType: "test");
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimus)));
        }
    }
}
