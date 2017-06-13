using Analyzer.Engine.BusinessAccessLayer;
using Analyzer.Engine.BusinessFacadeLayer;
using Analyzer.Engine.Common;
using Analyzer.Engine.DataAccessLayer.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineSite.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineSite.Controllers
{
    public class HomeController : Controller
    {
        private IOptions<ConfigSettings> _settings = null;

        public HomeController(IOptions<ConfigSettings> settings)
        {
            _settings = settings;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Auth()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            try
            {
                BusFacCore busFacCore = new BusFacCore(_settings);
                UtilsValidation utilsValidation = new UtilsValidation();
                BusUser busUser = new BusUser();
                if ((!string.IsNullOrEmpty(userModel.Username)) && (!string.IsNullOrEmpty(userModel.Password)) &&
                    (utilsValidation.IsValidEmail(userModel.Username)) && (busUser.IsValidPasswd(userModel.Password)))
                {
                    if (!(userModel.Password.Equals(userModel.ConfirmPassword, StringComparison.Ordinal)))
                    {
                        ViewData["PasswordMatch"] = true;
                    }
                    else
                    {
                        bool UserExist = busFacCore.Exist(userModel.Username);
                        if (!UserExist)
                        {
                            User user = busFacCore.UserCreate(userModel.Username, userModel.Password);
                            if ((user != null) && (user.UserID > 0))
                            {
                                return RedirectToAction("Login", userModel);
                            }
                            else
                            {
                                ViewData["HasError"] = true;
                            }
                        }
                        else
                        {
                            ViewData["UserExist"] = true;
                        }
                    }
                }
                else
                {
                    ViewData["InvalidCredentials"] = true;
                }


            }
            catch (Exception ex)
            {
                ViewData["HasError"] = true;
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            BusFacCore busFacCore = new BusFacCore(_settings);
            BusUser busUser = new BusUser();
            if ((!string.IsNullOrEmpty(userModel.Username)) && (!string.IsNullOrEmpty(userModel.Password)) &&
                (busUser.IsValidUsername(userModel.Username)) && (busUser.IsValidPasswd(userModel.Password)))
            {
                bool UserExist = busFacCore.Exist(userModel.Username);
                if (UserExist)
                {
                    User user = busFacCore.UserAuthenticate(userModel.Username, userModel.Password);
                    if ((user != null) && (user.UserID > 0))
                    {
                        userModel.DisplayName = userModel.Username;
                        if ((!string.IsNullOrEmpty(userModel.FirstName)) && (!string.IsNullOrEmpty(userModel.LastName)))
                        {
                            userModel.DisplayName = userModel.LastName + ", " + userModel.FirstName;
                        }
                        
                        var claims = new[] {
                            new Claim("name", userModel.Username),
                            new Claim(ClaimTypes.Name, userModel.DisplayName, ClaimValueTypes.String)
                        };

                        var principal = new ClaimsPrincipal(
                            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                        await HttpContext.Authentication.SignInAsync("MyCookieMiddlewareInstance", principal);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }

            ViewData["InvalidCredentials"] = true;
            return View("Index");
        }


        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.Authentication.SignOutAsync("MyCookieMiddlewareInstance");

        //    return RedirectToAction("Index", "Home");
        //}
    }
}
