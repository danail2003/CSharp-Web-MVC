using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string description, string userId, string repositoryId)
        {
            this.db.Commits.Add(new Commit
            {
                Description = description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                RepositoryId = repositoryId,
            });

            this.db.SaveChanges();
        }

        public IEnumerable<CommitsViewModel> GetAll(string userId)
        {
            return this.db.Commits.Where(x => x.CreatorId == userId).Select(x => new CommitsViewModel
            {
                Id = x.Id,
                Name = x.Repository.Name,
                CreatedOn = x.CreatedOn,
                Description = x.Description,
            }).ToList();
        }

        public CommitViewModel GetCommit(string id)
        {
            return this.db.Repositories.Where(x => x.Id == id).Select(x => new CommitViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).FirstOrDefault();
        }

        public void Remove(string id)
        {
            Commit commit = this.db.Commits.FirstOrDefault(x => x.Id == id);

            this.db.Commits.Remove(commit);

            this.db.SaveChanges();
        }
    }
}
