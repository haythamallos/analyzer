using Analyzer.Engine.BusinessFacadeLayer;
using Analyzer.Engine.Common;
using Analyzer.Engine.DataAccessLayer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
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
        private IHostingEnvironment _hostingEnv;

        public DashboardController(IOptions<ConfigSettings> settings, IHostingEnvironment env)
        {
            this._settings = settings;
            this._hostingEnv = env;
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
        public IActionResult ImageUpload()
        {
            long size = 0;
            var files = Request.Form.Files;
            if ((files != null) && (files.Count > 0))
            {
                var file = files[0];
                var filename = ContentDispositionHeaderValue
                           .Parse(file.ContentDisposition)
                           .FileName
                           .Trim('"');
                //filename = _hostingEnv.WebRootPath + $@"\{FileName}";
                filename = _hostingEnv.WebRootPath + $@"\photo-1.jpg";
                size += file.Length;
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }

            ViewBag.Message = $"{files.Count} file(s) / { size} bytes uploaded successfully!";

            return View();
        }
    }
}
