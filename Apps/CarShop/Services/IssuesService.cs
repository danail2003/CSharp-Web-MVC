namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Issues;
    using System.Linq;

    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext dbContext;

        public IssuesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string carId, string description)
        {
            this.dbContext.Issues.Add(new Issue
            {
                CarId = carId,
                Description = description,
                IsFixed = false,
            });

            this.dbContext.SaveChanges();
        }

        public void Delete(string issueId, string carId)
        {
            var issue = this.dbContext.Issues.FirstOrDefault(x => x.Id == issueId && x.CarId == carId);

            this.dbContext.Issues.Remove(issue);

            this.dbContext.SaveChanges();
        }

        public void Fix(string issueId, string carId)
        {
            var issue = this.dbContext.Issues.FirstOrDefault(x => x.Id == issueId && x.CarId == carId).IsFixed = true;

            this.dbContext.SaveChanges();
        }

        public IssueCarViewModel GetIssues(string carId)
        {
            return this.dbContext.Cars.Where(x => x.Id == carId).Select(x => new IssueCarViewModel
            {
                Id = x.Id,
                Year = x.Year,
                Model = x.Model,
                Issues = x.Issues.Select(i => new IssuesViewModel
                {
                    Id = i.Id,
                    IsFixed = i.IsFixed,
                    Description = i.Description,
                })
            }).FirstOrDefault();
        }
    }
}
