using FormManagement.Entities;

namespace FormManagement.Business
{
    public interface IUserService
    {
        User ValidateUser(string username, string password);
    }
}
