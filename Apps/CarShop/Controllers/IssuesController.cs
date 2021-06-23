using CarShop.Services;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;
        private readonly IUsersService usersService;

        public IssuesController(IIssuesService issuesService, IUsersService usersService)
        {
            this.issuesService = issuesService;
            this.usersService = usersService;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var viewModel = this.issuesService.GetIssues(carId);

            return this.View(viewModel);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(string carId, string description)
        {
            this.issuesService.Create(carId, description);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId, string carId)
        {
            var userId = this.User.Id;

            if (!this.usersService.IsUserMechanic(userId))
            {
                return this.Unauthorized();
            }

            this.issuesService.Fix(issueId, carId);

            return this.Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {
            this.issuesService.Delete(issueId, carId);

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
