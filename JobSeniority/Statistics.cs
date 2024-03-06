namespace JobSeniorityApp
{
    public class Statistics
    {
        public int Years
        {
            get
            {
                return this.yearsDurations + ((this.monthsDurations + this.daysDurations / 30) / 12);
            }
        }

        public int Months
        {
            get
            {
                return ((this.monthsDurations) + (this.daysDurations / 30)) % 12;
            }
        }

        public int Days
        {
            get
            {
                return this.daysDurations % 30;
            }
        }

        public int SumDays
        {
            get
            {
                return this.sumdays;
            }
        }

        public int NumberDurations { get; private set; }

        private int sumdays;
        private int yearsDurations;
        private int monthsDurations;
        private int daysDurations;

        public Statistics()
        {
            this.sumdays = 0;
            this.yearsDurations = 0;
            this.monthsDurations = 0;
            this.daysDurations = 0;
            this.NumberDurations = 0;
        }

        public void AddDuration(DateOnly beginDate, DateOnly endDate)
        {
            var beginDateTime = new DateTime(beginDate.Year, beginDate.Month, beginDate.Day, 0, 0, 0);
            var endDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0);
            TimeSpan interval = (endDateTime - beginDateTime);

            var yearsduration = YearsCalculate(beginDateTime, endDateTime);
            var monthsduration = MonthsCalculate(BeginDateForCalculationMonths(beginDateTime, yearsduration), endDateTime);
            var daysduration = DaysCalculate(BeginDateForCalculationDays(beginDateTime, yearsduration, monthsduration), endDateTime);

            if (this.daysDurations < 0)
            {
                --this.monthsDurations;
                this.daysDurations += 30;
            }

            this.sumdays += (int)interval.TotalDays + 1;
            this.yearsDurations += yearsduration;
            this.monthsDurations += monthsduration;
            this.daysDurations += daysduration;
            this.NumberDurations++;
        }

        private static int YearsCalculate(DateTime beginDateTime, DateTime endDateTime)
        {
            int counterYears = 0;
            DateTime pointerYears = beginDateTime;
            pointerYears = pointerYears.AddYears(1);
            while (endDateTime >= pointerYears)
            {
                counterYears++;
                pointerYears = pointerYears.AddYears(1);
            }
            return counterYears;
        }

        private int MonthsCalculate(DateTime beginDateTime, DateTime endDateTime)
        {
            int counterMonths = 0;
            DateTime pointerMonths = beginDateTime;
            pointerMonths = pointerMonths.AddMonths(1).AddDays(-1);
            while (endDateTime >= pointerMonths)
            {
                counterMonths++;
                pointerMonths = pointerMonths.AddMonths(1);
            }

            if (beginDateTime.Day == 1 && beginDateTime.Month == 1)
            {
                return --counterMonths;
            }
            else
            {
                return counterMonths;
            }
        }

        private static int DaysCalculate(DateTime begindateTime, DateTime endDateTime)
        {
            TimeSpan interval = endDateTime - begindateTime;
            return (int)interval.TotalDays + 1;
        }

        private static DateTime BeginDateForCalculationMonths(DateTime date, int years)
        {
            return date.AddYears(years);
        }

        private static DateTime BeginDateForCalculationDays(DateTime date, int years, int months)
        {
            if (date.Day == 1 && date.Month == 1)
            {
                return date.AddYears(years).AddMonths(months).AddDays(+1);
            }
            else
            {
                return date.AddYears(years).AddMonths(months);
            }
        }
    }
}
