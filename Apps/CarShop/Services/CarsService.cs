namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Cars;
    using System.Collections.Generic;
    using System.Linq;

    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext dbContext;

        public CarsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(AddCarInputModel model, string userId)
        {
            this.dbContext.Cars.Add(new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = userId,
            });

            this.dbContext.SaveChanges();
        }

        public IEnumerable<AllCarsViewModel> GetAllCars()
        {
            return this.dbContext.Cars.Where(x => x.Issues.Any(i => !i.IsFixed)).Select(x => new AllCarsViewModel
            {
                Model = x.Model,
                Year = x.Year,
                PlateNumber = x.PlateNumber,
                Id = x.Id,
                RemainingIssues = x.Issues.Where(i => !i.IsFixed).Count(),
                FixedIssues = x.Issues.Where(i => i.IsFixed).Count(),
                Image = x.PictureUrl
            }).ToList();
        }

        public IEnumerable<AllCarsViewModel> GetMyCars(string userId)
        {
            return this.dbContext.Cars.Where(x => x.OwnerId == userId).Select(x => new AllCarsViewModel
            {
                Model = x.Model,
                Year = x.Year,
                PlateNumber = x.PlateNumber,
                Id = x.Id,
                RemainingIssues = x.Issues.Where(i => !i.IsFixed).Count(),
                FixedIssues = x.Issues.Where(i => i.IsFixed).Count(),
                Image = x.PictureUrl
            }).ToList();
        }
    }
}
