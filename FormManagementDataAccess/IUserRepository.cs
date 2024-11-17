using FormManagement.Entities;

namespace FormManagement.DataAccess
{
    public interface IUserRepository
    {
        User ValidateUser(string username, string password);
    }
}
