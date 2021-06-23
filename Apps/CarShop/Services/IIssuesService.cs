namespace CarShop.Services
{
    using CarShop.ViewModels.Issues;

    public interface IIssuesService
    {
        IssueCarViewModel GetIssues(string carId);

        void Create(string carId, string description);

        void Fix(string issueId, string carId);

        void Delete(string issueId, string carId);
    }
}
