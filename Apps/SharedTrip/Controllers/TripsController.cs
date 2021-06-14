using SharedTrip.Services.Trips;
using SharedTrip.ViewModels.Trips;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService service;

        public TripsController(ITripsService service)
        {
            this.service = service;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.service.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(TripsInputModel model)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(model.StartPoint) || string.IsNullOrEmpty(model.EndPoint))
            {
                return this.View();
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(model.Description) || model.Description.Length > 80)
            {
                return this.View();
            }

            if (!DateTime.TryParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return this.View();
            }

            this.service.Create(model);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = this.service.GetTrip(tripId);

            return this.View(viewModel);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            string userId = this.GetUserId();

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (!this.service.HasSpace(tripId))
            {
                return this.View();
            }

            if (this.service.IsUserAddedToTrip(tripId, userId))
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.service.Join(tripId, userId);

            return this.Redirect("/");
        }
    }
}
