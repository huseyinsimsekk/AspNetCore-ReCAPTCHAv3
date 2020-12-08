using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecaptchaV3.Extensions
{
    public interface IRecaptchaExtension
    {
        Task<bool> VerifyAsync(string token);
    }
}
