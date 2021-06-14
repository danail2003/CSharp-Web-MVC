using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SharedTrip.Data;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services.Trips
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(TripsInputModel model)
        {
            this.db.Trips.Add(new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                Seats = model.Seats,
                Description = model.Description,
                ImagePath = model.ImagePath,
            });

            this.db.SaveChanges();
        }

        public IEnumerable<TripsViewModel> GetAll()
        {
            return this.db.Trips.Select(x => new TripsViewModel
            {
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime,
                Seats = x.Seats,
                Id = x.Id,
                UsedSeats = x.UserTrips.Count,
            })
                .ToList();
        }

        public TripViewModel GetTrip(string tripId)
        {
            return this.db.Trips.Where(x => x.Id == tripId).Select(x => new TripViewModel
            {
                Id = x.Id,
                Seats = x.Seats,
                DepartureTime = x.DepartureTime,
                Description = x.Description,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                ImagePath = x.ImagePath,
                UsedSeats = x.UserTrips.Count,
            }).FirstOrDefault();
        }

        public bool HasSpace(string tripId)
        {
            int space = this.db.Trips.FirstOrDefault(x => x.Id == tripId).Seats;

            return space > this.db.UserTrips.Where(x => x.TripId == tripId).Count();
        }

        public bool IsUserAddedToTrip(string tripId, string userId)
        {
            return this.db.UserTrips.Any(x => x.TripId == tripId && x.UserId == userId);
        }

        public void Join(string tripId, string userId)
        {
            this.db.UserTrips.Add(new UserTrip
            {
                TripId = tripId,
                UserId = userId,
            });

            this.db.SaveChanges();
        }
    }
}
