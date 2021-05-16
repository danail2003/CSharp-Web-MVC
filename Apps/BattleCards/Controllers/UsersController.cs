using System.ComponentModel.DataAnnotations;
using BattleCards.Services;
using BattleCards.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }

            if (string.IsNullOrEmpty(username))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(password))
            {
                return this.View();
            }

            if (!this.service.IsUsernameAndPasswordMatch(username, password))
            {
                return this.View();
            }

            string userId = this.service.GetUserId(username, password);

            this.SignIn(userId);

            return this.Redirect("/Cards/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }

            if (string.IsNullOrEmpty(model.Username) || model.Username.Length < 5 || model.Username.Length > 20)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.View();
            }

            if (this.service.IsEmailAvailable(model.Email))
            {
                return this.View();
            }

            if (this.service.IsUsernameAvailable(model.Username))
            {
                return this.View();
            }

            this.service.Register(model);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}
