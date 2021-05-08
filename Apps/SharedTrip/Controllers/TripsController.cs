using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Globalization;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService service;

        public TripsController(ITripsService service)
        {
            this.service = service;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You must login first.");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripsInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You must login first.");
            }

            if (string.IsNullOrEmpty(model.StartPoint))
            {
                return this.Error("Start Point is required.");
            }

            if (string.IsNullOrEmpty(model.EndPoint))
            {
                return this.Error("End Point is required.");
            }

            if (!DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return this.Error("Departure time must be in this format dd.MM.yyyy HH:mm");
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                return this.Error("Seats must be between 2 and 6.");
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > 80)
            {
                return this.Error("Description must be 80 symbols long.");
            }

            this.service.Add(model);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You must login first.");
            }

            var viewModel = this.service.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You must login first.");
            }

            var trip = this.service.GetTripById(tripId);

            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            string userId = this.GetUserId();

            if (!this.service.HasSpace(tripId))
            {
                return this.Error("No have enough space.");
            }

            if (this.service.HasAlreadyAddedUser(tripId, userId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.service.Join(tripId, userId);

            return this.Redirect("/Trips/All");
        }
    }
}
