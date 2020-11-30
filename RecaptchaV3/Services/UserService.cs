using RecaptchaV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecaptchaV3.Services
{
    public static class UserService
    {
        private static User ValidUser = new User() { Name = "huseyin", Password = "password" };
        public static bool IsValid(User user)
        {
            if (user == null) { return false; }

            if (ValidUser.Name == user.Name && ValidUser.Password == user.Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
