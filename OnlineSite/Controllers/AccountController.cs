using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineSite.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Auth()
        {
           
            return View();
        }

        public async Task<IActionResult> Login()
        {
            //var claims = new[] {
            //    new Claim("name", "InsertAccountNameHere"),
            //    new Claim(ClaimTypes.Role, "InsertRoleHere")
            //};

            var claims = new[] {
                new Claim("name", "InsertAccountNameHere")
            };

            var principal = new ClaimsPrincipal(
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal);

            return Ok();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");

            return Ok();
        }
    }
}
