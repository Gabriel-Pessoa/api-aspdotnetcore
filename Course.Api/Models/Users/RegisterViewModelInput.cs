using System.ComponentModel.DataAnnotations;

namespace Course.Api.Models.Users
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage = "The login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password is required")]
        public string Password { get; set; }


    }
}
