using Suls.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Suls.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemsService service;

        public HomeController(IProblemsService service)
        {
            this.service = service;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            var viewModel = this.service.GetAll();

            if (this.IsUserSignedIn())
            {
                return this.View(viewModel, "IndexLoggedIn");
            }

            return this.View(viewModel);
        }
    }
}
