using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tasty.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podaj login.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Podaj hasło.")]
        [UIHint("password")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "/";
    }
}
