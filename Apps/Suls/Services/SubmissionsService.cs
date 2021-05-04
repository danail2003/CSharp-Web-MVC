using System;
using System.Linq;
using Suls.Data;

namespace Suls.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly ApplicationDbContext db;
        private readonly Random random;

        public SubmissionsService(ApplicationDbContext db)
        {
            this.db = db;
            this.random = new Random();
        }

        public void Create(string userId, string problemId, string code)
        {
            int maxPoints = this.db.Problems.Where(x => x.Id == problemId).Select(x => x.Points).FirstOrDefault();

            this.db.Add(new Submission
            {
                UserId = userId,
                ProblemId = problemId,
                Code = code,
                CreatedOn = DateTime.Now,
                AchievedResult = random.Next(0, maxPoints + 1)
            });

            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            Submission submission = this.db.Submissions.Find(id);

            this.db.Submissions.Remove(submission);

            this.db.SaveChanges();
        }
    }
}
