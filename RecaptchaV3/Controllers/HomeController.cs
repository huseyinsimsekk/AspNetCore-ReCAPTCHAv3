using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RecaptchaV3.Extensions;
using RecaptchaV3.Models;
using RecaptchaV3.Services;

namespace RecaptchaV3.Controllers
{
    public class HomeController : Controller
    {
        private IRecaptchaExtension _recaptcha;
        public HomeController(IRecaptchaExtension recaptcha)
        {
            _recaptcha = recaptcha;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid && UserService.IsValid(user))
            {
                return RedirectToAction("Welcome", "Home");
            }
            return View();
        }

        public IActionResult Welcome()
        {
            ViewData["Message"] = $"You are online now! Date:{DateTime.Now.ToShortTimeString()}";
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Verify(string token)
        {
            var verified = await _recaptcha.Verify(token);

            return Json(verified);
        }
    }
}
