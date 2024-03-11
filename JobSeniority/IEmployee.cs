using static JobSeniorityApp.EmployeeBase;

namespace JobSeniorityApp
{
    public interface IEmployee
    {
        public string Name { get; }

        public string Surname { get; }

        public event DurationAdddedDelegate DurationAdded;

        public void AddDuration(DateOnly beginDate, DateOnly endDate);

        public void ShowDurations();

        Statistics GetStatistics();

        public void ShowStatistics();

    }
}