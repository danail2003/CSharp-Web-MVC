using Suls.ViewModels.Submissions;
using System.Collections.Generic;

namespace Suls.ViewModels.Problems
{
    public class ProblemViewModel
    {
        public string Name { get; set; }

        public ICollection<SubmissionsViewModel> Submissions { get; set; }
    }
}
