using System.Collections.Generic;
using System.Linq;
using Suls.Data;
using Suls.ViewModels.Problems;
using Suls.ViewModels.Submissions;

namespace Suls.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, int totalPoints)
        {
            this.db.Problems.Add(new Problem
            {
                Name = name,
                Points = totalPoints
            });

            this.db.SaveChanges();
        }

        public IEnumerable<ProblemsViewModel> GetAll()
        {
            return this.db.Problems.Select(x => new ProblemsViewModel()
            {
                Name = x.Name,
                Id = x.Id,
                Count = x.Submissions.Count
            }).ToList();
        }

        public string GetName(string id)
        {
            return this.db.Problems.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault();
        }

        public ProblemViewModel GetSubmissions(string id)
        {
            return this.db.Problems.Where(x => x.Id == id).Select(x => new ProblemViewModel
            {
                Name = x.Name,
                Submissions = x.Submissions.Select(y => new SubmissionsViewModel
                {
                    SubmissionId = y.Id,
                    CreatedOn = y.CreatedOn,
                    MaxPoints = x.Points,
                    Username = y.User.Username,
                    AchievedResult = y.AchievedResult,
                }).ToList()
            }).FirstOrDefault();
        }
    }
}
