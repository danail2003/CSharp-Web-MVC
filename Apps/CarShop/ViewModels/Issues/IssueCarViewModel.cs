namespace CarShop.ViewModels.Issues
{
    using System.Collections.Generic;

    public class IssueCarViewModel
    {
        public string Id { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public IEnumerable<IssuesViewModel> Issues { get; set; }
    }
}
