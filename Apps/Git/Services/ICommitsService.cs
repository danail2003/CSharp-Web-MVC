using System.Collections.Generic;
using Git.ViewModels.Commits;

namespace Git.Services
{
    public interface ICommitsService
    {
        void Create(string description, string userId, string repositoryId);

        void Remove(string id);

        CommitViewModel GetCommit(string id);

        IEnumerable<CommitsViewModel> GetAll(string userId);
    }
}
