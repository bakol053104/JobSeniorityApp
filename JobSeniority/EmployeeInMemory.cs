namespace JobSeniorityApp
{
    public class EmployeeInMemory : EmployeeBase
    {
        private readonly List<Duration> durations = new List<Duration>();

        public EmployeeInMemory(string name, string surname)
             : base(name, surname)
        {

        }

        public override void AddDuration(DateOnly beginDate, DateOnly endDate)
        {
            if (IsRangeDatesValid(beginDate, endDate))
            {
                var duration = new Duration(beginDate, endDate);
                durations.Add(duration);
                CallEventDurationAdded();
            }
            else
            {
                throw new Exception($"\tPodane daty zawierają się w poprzednich okresach pracy\n ");
            }

        }

        public override void ShowDurations()
        {
            var index = 1;
            foreach (var duration in this.durations)
            {
                Console.WriteLine($"\tOkres [{index}]: {duration.beginDate} - {duration.endDate}\n");
                index++;
            }
        }

        public override Statistics GetStatistics()
        {
            Statistics statistics = new Statistics();

            if (this.durations.Count > 0)
            {
                foreach (var duration in this.durations)
                {
                    statistics.AddDuration(duration.beginDate, duration.endDate);
                }
            }
            else
            {
                throw new Exception("\t[Brak wprowadzonych okresów pracy]");
            }
            return statistics;
        }

        protected override bool IsRangeDatesValid(DateOnly beginDate, DateOnly endDate)
        {
            foreach (var duration in this.durations)
            {
                if (beginDate >= duration.beginDate && beginDate <= duration.endDate || endDate >= duration.beginDate && endDate <= duration.endDate)
                {
                    return false;
                }
                else if (duration.beginDate >= beginDate && duration.endDate <= endDate || duration.beginDate >= endDate && duration.endDate <= endDate)
                {
                    return false;
                }
            }
            return true;
        }

        private struct Duration
        {
            public DateOnly beginDate;
            public DateOnly endDate;

            public Duration(DateOnly beginDate, DateOnly endDate)
            {
                this.beginDate = beginDate;
                this.endDate = endDate;
            }
        }
    }
}

