using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecaptchaV3.Models
{
    public class User
    {
        [Display(Name="Name")]
        public string Name { get; set; }
        [Display(Name ="Password")]
        public string Password { get; set; }

    }
}
