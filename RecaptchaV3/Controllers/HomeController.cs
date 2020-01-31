using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecaptchaV3.Models;

namespace RecaptchaV3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private static string googleSecretKey = "YOUR-SECRETKEY-HERE";
        private static string googleRecaptchaVerifyApi = "https://www.google.com/recaptcha/api/siteverify";
        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UserModel model)
        {
            var validUser = new UserModel() { Name = "huseyin", Password = "passwd" };
            if (ModelState.IsValid)
            {
                if (validUser.Name == model.Name && validUser.Password == model.Password)
                    return RedirectToAction("Welcome", "Home");
                else
                    return View();
            }
            return View();
        }

        public IActionResult Welcome()
        {
            ViewData["Message"] = "You are online now!";

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> TokenVerify(string token)
        {
            var verified = true;
            TokenResponseModel tokenResponse = new TokenResponseModel() { Success = false };

            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetStringAsync($"{googleRecaptchaVerifyApi}?secret={googleSecretKey}&response={token}");
                tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(response);
            }
            // Recaptcha V3 Verify Api send score 0-1. If score is low such as less than 0.5, you can think that it is a bot and return false.     
            // If token is not success then return false
            if (!tokenResponse.Success || tokenResponse.Score < (decimal)0.5)
                verified = false;
            return Json(verified);
        }
    }
}
