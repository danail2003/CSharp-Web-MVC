namespace CarShop
{
    using System.Threading.Tasks;
    using CarShop.Data;
    using CarShop.Services;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer;
    using MyWebServer.Controllers;
    using MyWebServer.Results.Views;

    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IUsersService, UsersService>()
                    .Add<ICarsService, CarsService>()
                    .Add<IIssuesService, IssuesService>()
                    .Add<ApplicationDbContext>())
                .WithConfiguration<ApplicationDbContext>(context => context.Database.Migrate())
                .Start();
    }
}