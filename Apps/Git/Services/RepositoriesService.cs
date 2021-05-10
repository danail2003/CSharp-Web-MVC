using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string repositoryType, string ownerId)
        {
            this.db.Repositories.Add(new Repository
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = true && repositoryType == "Public",
                OwnerId = ownerId
            });

            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            return this.db.Repositories.Where(x => x.IsPublic).Select(x => new RepositoryViewModel
            {
                Id = x.Id,
                Commits = x.Commits.Count,
                Owner = x.Owner.Username,
                CreatedOn = x.CreatedOn,
                Name = x.Name
            }).ToList();
        }
    }
}
