using Git.ViewModels.Users;

namespace Git.Services
{
    public interface IUsersService
    {
        void Register(RegisterInputModel model);

        string GetUserId(string username, string password);

        bool IsUsernameAndPasswordMatch(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);
    }
}
