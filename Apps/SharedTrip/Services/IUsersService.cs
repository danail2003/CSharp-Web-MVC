using SharedTrip.ViewModels;

namespace SharedTrip.Services
{
    public interface IUsersService
    {
        void Register(RegisterInputModel model);

        bool IsEmailAvailable(string email);

        string GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsUsernameAndPasswordMatch(string username, string password);
    }
}
