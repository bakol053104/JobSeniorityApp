using System.Globalization;

namespace JobSeniorityApp
{
    public abstract class EmployeeBase : IEmployee
    {

        public delegate void DurationAdddedDelegate(object sender, EventArgs args);

        public event DurationAdddedDelegate DurationAdded;

        public EmployeeBase(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public abstract void AddDuration(DateOnly begindate, DateOnly endDate);

        public abstract Statistics GetStatistics();

        public abstract void ShowDurations();

        protected abstract bool IsRangeDatesValid(DateOnly beginDate, DateOnly endDate);

        public void AddDuration(string begin, string end)
        {
            DateOnly beginDate;
            DateOnly endDate;

            if (!DateOnly.TryParseExact(begin, "d", CultureInfo.CurrentCulture, 0, out beginDate))
            {
                throw new Exception($"\tWprowadzono nieprawidłową datę początkową");
            }
            else if (!DateOnly.TryParseExact(end, "d", CultureInfo.CurrentCulture, 0, out endDate))
            {
                throw new Exception($"\tWprowadzono nieprawidłową datę końcową");
            }
            else if (beginDate > endDate)
            {
                throw new Exception($"\tData końcowa jest wcześniejsza niż początkowa");
            }
            else if (endDate > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new Exception($"\tData końcowa wykracza poza dzisiejszą datę");
            }
            else
            {
                AddDuration(beginDate, endDate);
            }
        }

        public void ShowStatistics()
        {
            var employeestat = GetStatistics();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n\n\tSzczegółowe wyniki stażu pracy dla pracownika {Name} {Surname}:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n\t------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"\tLat\t|\t  Miesięcy\t|\tDni");
            Console.Write($"\t|\tDni (w sumie)\t|\tLiczba okresów");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n\t------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"\t{employeestat.Years}\t|\t     {employeestat.Months}        \t|\t{employeestat.Days} ");
            Console.Write($"\t|\t       {employeestat.SumDays}   \t|\t      {employeestat.NumberDurations}");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n\t------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            ShowDurations();
            Console.ResetColor();
        }

        protected void CallEventDurationAdded()
        {
            if (DurationAdded != null)
            {
                DurationAdded(this, new EventArgs());
            }
        }
    }
}
