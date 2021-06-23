namespace CarShop.Controllers
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using CarShop.Services;
    using CarShop.ViewModels.Cars;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class CarsController : Controller
    {
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;

        public CarsController(ICarsService carsService, IUsersService usersService)
        {
            this.carsService = carsService;
            this.usersService = usersService;
        }

        [Authorize]
        public HttpResponse All()
        {
            var userId = this.User.Id;

            IEnumerable<AllCarsViewModel> viewModel;
            if (this.usersService.IsUserMechanic(userId))
            {
                viewModel = this.carsService.GetAllCars();
            }
            else
            {
                viewModel = this.carsService.GetMyCars(userId);
            }

            return this.View(viewModel);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.usersService.IsUserMechanic(this.User.Id))
            {
                return this.Unauthorized();
            }

            return this.View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddCarInputModel model)
        {
            string userId = this.User.Id;

            if (this.usersService.IsUserMechanic(userId))
            {
                return this.Unauthorized();
            }

            if (string.IsNullOrEmpty(model.Model) || model.Model.Length < 5 || model.Model.Length > 20)
            {
                return this.Error("Model must be between 5 and 20 symbols long");
            }

            if (string.IsNullOrEmpty(model.Image))
            {
                return this.Error("Image is required.");
            }

            if (string.IsNullOrEmpty(model.PlateNumber) || !Regex.IsMatch(model.PlateNumber, @"[A-Z]{2}[0-9]{4}[A-Z]{2}"))
            {
                return this.Error("Plate Number must be in format AA1111AA");
            }

            this.carsService.Create(model, userId);

            return this.Redirect("/Cars/All");
        }
    }
}
