using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        public HttpResponse Add()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You must login first.");
            }

            return this.View();
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You must login first.");
            }

            return this.View();
        }

        public HttpResponse Details()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Error("You must login first.");
            }

            return this.View();
        }
    }
}
