using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Api.Models.Users
{
    public class LoginViewModelInput
    {
        [Required(ErrorMessage = "The login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
    }
}
