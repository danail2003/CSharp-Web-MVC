using Suls.ViewModels;

namespace Suls.Services
{
    public interface IUsersService
    {
        void Register(RegisterUserInputModel model);

        bool IsEmailAvailable(string email);

        bool IsUsernameAvailable(string username);

        bool IsUsernameAndPasswordMatch(string username, string password);

        string GetUserId(string username, string password);
    }
}
