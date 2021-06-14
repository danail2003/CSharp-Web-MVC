using SharedTrip.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services.Users
{
    public interface IUsersService
    {
        void Register(RegisterInputModel model);

        string GetUserId(LoginInputModel model);

        bool IsUsernameAndPasswordMatch(LoginInputModel model);

        bool IsUsernameAvailable(string username);

        bool IsEmailAvailable(string email);
    }
}
