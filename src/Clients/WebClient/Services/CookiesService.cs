using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace WebClient.Services {
    partial class CookiesService {

        //This method creates the claims and the cookie for a user session.
        public static async Task CreateAuthenticationCookies(HttpContext oHttpContext)  {

            var claims = new List<Claim> {
                new (ClaimTypes.Role, "1"),
                new (ClaimTypes.NameIdentifier, "1"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await oHttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                                        new ClaimsPrincipal(claimsIdentity));
        }

        //This method deletes cookies from the session.
        public static async Task RemoveAuthenticationCookie(HttpContext oHttpContext) 
            => await oHttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}