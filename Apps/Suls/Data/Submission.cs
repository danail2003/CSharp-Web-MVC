using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suls.Data
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required, MaxLength(800)]
        public string Code { get; set; }

        [MaxLength(300)]
        public int AchievedResult { get; set; }

        public DateTime CreatedOn { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public User User { get; set; }

        [ForeignKey(nameof(Problem))]
        public string ProblemId { get; set; }

        public Problem Problem { get; set; }
    }
}
