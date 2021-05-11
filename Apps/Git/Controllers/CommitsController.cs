using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService service;

        public CommitsController(ICommitsService service)
        {
            this.service = service;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            string id = this.GetUserId();

            var viewModel = this.service.GetAll(id);

            return this.View(viewModel);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.service.GetCommit(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string description, string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.service.GetCommit(id);

            if (string.IsNullOrEmpty(description) || description.Length < 5)
            {
                return this.View(viewModel);
            }

            string userId = this.GetUserId();

            this.service.Create(description, userId, id);

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.service.Remove(id);

            return this.Redirect("/Commits/All");
        }
    }
}
