using System;

namespace Suls.ViewModels.Submissions
{
    public class SubmissionsViewModel
    {
        public string SubmissionId { get; set; }

        public string Username { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public int Percentage => this.AchievedResult *100 / this.MaxPoints;

        public DateTime CreatedOn { get; set; }
    }
}
