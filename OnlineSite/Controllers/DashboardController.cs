using Analyzer.Engine.BusinessFacadeLayer;
using Analyzer.Engine.Common;
using Analyzer.Engine.DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineSite.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineSite.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private IOptions<ConfigSettings> _settings = null;

        public DashboardController(IOptions<ConfigSettings> settings)
        {
            _settings = settings;
        }

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
            string email = Username;
            BusFacCore busFacCore = new BusFacCore(_settings);
            User user = busFacCore.UserGet(Username);
            if (user != null)
            {
                //ProfileModel model = Cnv.MtoD(user);
            }
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

        [HttpPost]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            //if (file.Length > 0)
            //{
            //    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
            //    {
            //        await file.CopyToAsync(fileStream);
            //    }
            //}
            return View();
        }
    }
}
