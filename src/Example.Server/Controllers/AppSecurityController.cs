using Conjure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Example.Server.Controllers
{
    [ApiController]
    [Route("api/security")]
    public class AppSecurityController : Controller
    {
        private static readonly AppUser AnonymousUser = new AppUser { IsAuthenticated = false, Name = "ServerAnon", };

        [HttpGet("user")]
        public AppUser GetUser()
        {
            return User.Identity.IsAuthenticated
                ? new AppUser { IsAuthenticated = true, Name = User.Identity.Name, }
                : AnonymousUser;
        }

        [HttpGet("user/signin")]
        public async Task SignIn(string redirectUri)
        {
            if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
            {
                redirectUri = "/";
            }

            await HttpContext.ChallengeAsync(
                new AuthenticationProperties { RedirectUri = redirectUri });
        }

        [HttpGet("user/signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }

        [HttpGet("user/fake-signin")]
        public async Task<IActionResult> FakeSignIn(string redirectUri)
        {
            if (string.IsNullOrEmpty(redirectUri) || !Url.IsLocalUrl(redirectUri))
            {
                redirectUri = "/";
            }

            // Fake Login
            var identity = new ClaimsIdentity("fake");
            identity.AddClaim(new Claim(ClaimTypes.Name, "FakeUser1"));
            await HttpContext.SignInAsync("fake", new ClaimsPrincipal(identity));

            return Redirect(redirectUri);
        }

        [HttpGet("user/fake-signout")]
        public async Task<IActionResult> FakeSignOut()
        {
            await HttpContext.SignOutAsync("fake");

            return Redirect("~/");
        }
    }
}
