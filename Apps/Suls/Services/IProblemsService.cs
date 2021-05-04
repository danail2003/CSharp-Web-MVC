using Suls.ViewModels.Problems;
using System.Collections.Generic;

namespace Suls.Services
{
    public interface IProblemsService
    {
        void Create(string name, int totalPoints);

        IEnumerable<ProblemsViewModel> GetAll();

        ProblemViewModel GetSubmissions(string id);

        string GetName(string id);
    }
}
