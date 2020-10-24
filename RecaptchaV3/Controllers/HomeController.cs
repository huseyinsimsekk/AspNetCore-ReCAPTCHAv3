using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RecaptchaV3.Models;

namespace RecaptchaV3.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration { get; }

        private readonly IHttpClientFactory _clientFactory;
        private static string googleSecretKey { get; set; }
        private static string googleRecaptchaVerifyApi { get; set; }
        public HomeController(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _configuration = configuration;
            _clientFactory = clientFactory;
            googleSecretKey = configuration.GetSection("GoogleRecaptcha").GetSection("Secretkey").Value ?? "";
            googleRecaptchaVerifyApi = configuration.GetSection("GoogleRecaptcha").GetSection("VefiyAPIAddress").Value ?? "";
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
