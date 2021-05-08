using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(TripsInputModel model)
        {
            this.db.Trips.Add(new Trip
            {
                ImagePath = model.ImagePath,
                DepartureTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                Description = model.Description,
                Seats = model.Seats
            });

            this.db.SaveChanges();
        }

        public IEnumerable<TripsViewModel> GetAll()
        {
            return this.db.Trips.Select(x => new TripsViewModel
            {
                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime,
                UsedSeats = x.UserTrips.Count,
                Seats = x.Seats
            }).ToList();
        }

        public TripViewModel GetTripById(string id)
        {
            return this.db.Trips.Where(x => x.Id == id).Select(x => new TripViewModel
            {
                Id = x.Id,
                Description = x.Description,
                DepartureTime = x.DepartureTime,
                UsedSeats = x.UserTrips.Count,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                ImagePath = x.ImagePath,
                Seats = x.Seats
            }).FirstOrDefault();
        }

        public bool HasAlreadyAddedUser(string tripId, string userId)
        {
            return this.db.UserTrips.Any(x => x.TripId == tripId && x.UserId == userId);
        }

        public bool HasSpace(string tripId)
        {
            int space = this.db.Trips.FirstOrDefault(x => x.Id == tripId).Seats;

            return this.db.UserTrips.Where(x => x.TripId == tripId).Count() < space;
        }

        public void Join(string tripId, string userId)
        {
            this.db.UserTrips.Add(new UserTrip
            {
                TripId = tripId,
                UserId = userId
            });

            this.db.SaveChanges();
        }
    }
}
