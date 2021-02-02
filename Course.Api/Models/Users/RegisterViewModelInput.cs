using System.ComponentModel.DataAnnotations;

namespace Course.Api.Models.Users
{
    public class RegisterViewModelInput
    {
        [Required(ErrorMessage = "The login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The confirmPassword is required")]
        [Compare(nameof(Password), ErrorMessage = "The passwords mismatch")]
        public string ConfirmPassword { get; set; }
    }
}
