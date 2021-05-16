using BattleCards.ViewModels.Users;

namespace BattleCards.Services
{
    public interface IUsersService
    {
        void Register(RegisterInputModel model);

        string GetUserId(string username, string password);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);

        bool IsUsernameAndPasswordMatch(string username, string password);
    }
}
