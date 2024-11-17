using FormManagement.DataAccess;
using FormManagement.Entities;

namespace FormManagement.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User ValidateUser(string username, string password)
        {
            return _userRepository.ValidateUser(username, password);
        }
    }
}
