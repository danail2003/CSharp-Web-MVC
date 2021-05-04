using Suls.Services;
using Suls.ViewModels.Submissions;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Suls.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService service;
        private readonly IProblemsService problemsService;

        public SubmissionsController(ISubmissionsService service, IProblemsService problemsService)
        {
            this.service = service;
            this.problemsService = problemsService;
            this.problemsService = problemsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var viewModel = new CreateViewModel
            {
                ProblemId = id,
                Name = this.problemsService.GetName(id)
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(code) || code.Length < 30 || code.Length > 800)
            {
                return this.Redirect("/");
            }

            string userId = this.GetUserId();

            this.service.Create(userId, problemId, code);

            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            this.service.Delete(id);

            return this.Redirect("/");
        }
    }
}
