using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Podaj login.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Podaj hasło.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Podaj email.")]
        [UIHint("email")]
        public string Email { get; set; }
        [UIHint("tel")]
        public string Phone { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string ReturnUrl { get; set; }
    }
}
