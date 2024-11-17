using FormManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace FormManagement.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly FormDbContext _context;

        public UserRepository(FormDbContext context)
        {
            _context = context;
        }

        public User ValidateUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
