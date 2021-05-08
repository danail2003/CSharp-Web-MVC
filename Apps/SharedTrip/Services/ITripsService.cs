using System.Collections.Generic;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Add(TripsInputModel model);

        IEnumerable<TripsViewModel> GetAll();

        TripViewModel GetTripById(string id);

        bool HasAlreadyAddedUser(string tripId, string userId);

        bool HasSpace(string tripId);

        void Join(string tripId, string userId);
    }
}
