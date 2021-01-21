using System.ComponentModel.DataAnnotations;

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
