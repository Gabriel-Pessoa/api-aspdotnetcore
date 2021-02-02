using Course.Api.Business.Entities;
using Course.Api.Models.Users;

namespace Course.Api.Business.Repositories
{
    public interface IUserRepository
    {
        void Add(UserEntity user);
        void Commit();
        UserEntity GetUser(string login);
        bool Authenticate(UserEntity user, LoginViewModelInput loginViewModelInput);
    }
}