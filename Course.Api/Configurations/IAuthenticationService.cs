using Course.Api.Models.Users;

namespace Course.Api.Configurations
{
    public interface IAuthenticationService
    {
        string GetToken(UserViewModelOutput userViewModelOutput);
    }
}