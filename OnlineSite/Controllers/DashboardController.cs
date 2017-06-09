using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineSite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSite.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public string Username
        {
            get {
                var claims = ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims;
                var claim = claims.SingleOrDefault(m => m.Type == "name");
                return(claim.Value);
            }
        }

        public IActionResult Index()
        {
            string email = Username;
            return View();
        }
        public IActionResult Documentation()
        {
            return View();
        }
        public IActionResult Faqs()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Profile()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Profile(ProfileModel model)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");

            return RedirectToAction("Index", "Home");
        }
    }
}
