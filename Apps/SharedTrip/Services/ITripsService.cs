using System.Collections.Generic;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Add(TripsInputModel model);

        IEnumerable<TripsViewModel> GetAll();

        TripViewModel GetTripById(string id);
    }
}
