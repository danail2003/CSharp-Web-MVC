using SharedTrip.ViewModels.Trips;
using System.Collections.Generic;

namespace SharedTrip.Services.Trips
{
    public interface ITripsService
    {
        IEnumerable<TripsViewModel> GetAll();

        void Create(TripsInputModel model);

        TripViewModel GetTrip(string tripId);

        bool IsUserAddedToTrip(string tripId, string userId);

        bool HasSpace(string tripId);

        void Join(string tripId, string userId);
    }
}
