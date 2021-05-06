using System.ComponentModel.DataAnnotations;
using SharedTrip.Services;
using SharedTrip.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
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
                return this.Error("You've already logged in.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You've already loggedIn.");
            }

            if (string.IsNullOrEmpty(username))
            {
                return this.Error("Username must be valid.");
            }

            if (string.IsNullOrEmpty(password))
            {
                return this.Error("Password must be valid.");
            }

            if (!this.service.IsUsernameAndPasswordMatch(username, password))
            {
                return this.Error("Username or Password doesn't match.");
            }

            string userId = this.service.GetUserId(username, password);

            this.SignIn(userId);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Register()
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You've already registered.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (this.IsUserSignedIn())
            {
                return this.Error("You've already registered.");
            }

            if (string.IsNullOrEmpty(model.Username) || model.Username.Length < 5 || model.Username.Length > 20)
            {
                return this.Error("Username must be between 5 and 20 symbols long.");
            }

            if (string.IsNullOrEmpty(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return this.Error("Email must be valid.");
            }

            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6 || model.Password.Length > 20)
            {
                return this.Error("Password must be between 6 and 20 symbols long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Password and Confirm Password must be same.");
            }

            if (this.service.IsUsernameAvailable(model.Username))
            {
                return this.Error("Username already exists.");
            }

            if (this.service.IsEmailAvailable(model.Email))
            {
                return this.Error("Email already exists.");
            }

            this.service.Register(model);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You can't logout because you're not logged in.");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}
