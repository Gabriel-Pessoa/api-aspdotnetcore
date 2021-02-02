using BC = BCrypt.Net.BCrypt;
using System.Linq;
using Course.Api.Business.Entities;
using Course.Api.Business.Repositories;
using Course.Api.Models.Users;

namespace Course.Api.Infraestruture.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CourseDbContext _context;

        public UserRepository(CourseDbContext context)
        {
            _context = context;
        }

        public void Add(UserEntity user)
        {
            _context.User.Add(user);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public UserEntity GetUser(string login)
        {
            return _context.User.FirstOrDefault(u => u.Login == login);
        }

        public bool Authenticate(UserEntity user, LoginViewModelInput loginViewModelInput)
        {
            if (user == null || !BC.Verify(loginViewModelInput.Password, user.Password))
            {
                return false;
            }
            return true;
        }
    }
}