using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService service;

        public RepositoriesController(IRepositoriesService service)
        {
            this.service = service;
        }

        public HttpResponse All()
        {
            var viewModel = this.service.GetAll();

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(name) || name.Length < 3 || name.Length > 10)
            {
                return this.View();
            }

            if (string.IsNullOrEmpty(repositoryType) || repositoryType != "Public" && repositoryType != "Private")
            {
                return this.View();
            }

            string ownerId = this.GetUserId();

            this.service.Create(name, repositoryType, ownerId);

            return this.Redirect("/Repositories/All");
        }
    }
}
