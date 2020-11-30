using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RecaptchaV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecaptchaV3.Extensions
{
    public class RecaptchaExtension: IRecaptchaExtension
    {
        private IConfiguration _configuration { get; }
        private static string GoogleSecretKey { get; set; }
        private static string GoogleRecaptchaVerifyApi { get; set; }
        private static decimal RecaptchaThreshold { get; set; }
        public RecaptchaExtension()
        {
            _configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            GoogleRecaptchaVerifyApi = _configuration.GetSection("GoogleRecaptcha").GetSection("VefiyAPIAddress").Value ?? "";
            GoogleSecretKey = _configuration.GetSection("GoogleRecaptcha").GetSection("Secretkey").Value ?? "";

            var hasThresholdValue = decimal.TryParse(_configuration.GetSection("RecaptchaThreshold").Value ?? "", out var threshold);
            if (hasThresholdValue)
            {
                RecaptchaThreshold = threshold;
            }
        }
        public async Task<bool> Verify(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                throw new Exception("Token cannot be null!");
            }
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync($"{GoogleRecaptchaVerifyApi}?secret={GoogleSecretKey}&response={token}");
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(response);
                if (!tokenResponse.Success || tokenResponse.Score < RecaptchaThreshold)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
