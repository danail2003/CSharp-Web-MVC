namespace CarShop.Controllers
{
    using CarShop.Services;
    using CarShop.ViewModels.Users;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(model.Username) || model.Username.Length < 4 || model.Username.Length > 20)
            {
                return this.Error("Username must be between 4 and 20 symbols long.");
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                return this.Error("Email is required");
            }

            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 5 || model.Password.Length > 20)
            {
                return this.Error("Password must be between 5 and 20 symbols long.");
            }

            if (this.usersService.IsUsernameAvailable(model.Username))
            {
                return this.Error("Username is already exists.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Passwords must be same");
            }

            this.usersService.Create(model.Username, model.Email, model.Password, model.UserType);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(LoginInputModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return this.Error("Username or password is not valid.");
            }

            this.SignIn(this.usersService.GetUserId(model.Username, model.Password));

            return this.Redirect("/Cars/All");
        }

        public HttpResponse Logout()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();

            return this.Redirect("/");
        }
    }
}
