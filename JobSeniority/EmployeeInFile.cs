using System.Text;

namespace JobSeniorityApp
{
    public class EmployeeInFile : EmployeeBase
    {
        private const string fileEmployeesList = "Lista_pracownikow.txt";
        private const string fileAddString = "_okresy.txt";
        private readonly string fileNameForDuration;

        public EmployeeInFile(string name, string surname)
       : base(name, surname)
        {
            fileNameForDuration = name + surname + fileAddString;

            if (File.Exists(fileNameForDuration))
            {
                File.Delete(fileNameForDuration);
            }

        }

        public override void AddDuration(DateOnly beginDate, DateOnly endDate)
        {
            if (!File.Exists(fileNameForDuration))
            {
                using (var writer = File.AppendText(fileNameForDuration))
                {
                    writer.WriteLine($"{beginDate} {endDate}");
                }
                CallEventDurationAdded();
            }
            else
            {
                if (IsRangeDatesValid(beginDate, endDate))
                {
                    using (var writer = File.AppendText(fileNameForDuration))
                    {
                        writer.WriteLine($"{beginDate} {endDate}");
                    }
                    CallEventDurationAdded();
                }
                else
                {
                    throw new Exception($"\tPodane daty zawierają się w poprzednich okresach pracy\n");
                }
            }

        }

        public override Statistics GetStatistics()
        {
            Statistics statistics = new Statistics();

            if (File.Exists(fileNameForDuration))
            {
                StringBuilder sb = new StringBuilder($"{this.Name}|{this.Surname}");
                using (var writer = File.AppendText(fileEmployeesList))
                using (var reader = File.OpenText(fileNameForDuration))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] subsLine = line.Split();
                        var beginDateFile = DateOnly.Parse(subsLine[0]);
                        var endDateFile = DateOnly.Parse(subsLine[1]);
                        sb.Append($"|{beginDateFile} {endDateFile}");
                        statistics.AddDuration(beginDateFile, endDateFile);
                        line = reader.ReadLine();
                    }
                    sb.Append($"|");
                    writer.WriteLine(sb);
                }
            }
            else
            {
                throw new Exception("\t[Brak wprowadzonych okresów pracy]");
            }
            return statistics;
        }

        public override void ShowDurations()
        {
            if (File.Exists(fileNameForDuration))
            {
                var index = 1;
                using (var reader = File.OpenText(fileNameForDuration))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] subsLine = line.Split();
                        Console.WriteLine($"\tOkres [{index}]: {subsLine[0]} - {subsLine[1]}\n");
                        index++;
                        line = reader.ReadLine();
                    }
                }
            }
        }

        protected override bool IsRangeDatesValid(DateOnly beginDate, DateOnly endDate)
        {
            if (File.Exists(fileNameForDuration))
            {
                using (var reader = File.OpenText(fileNameForDuration))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] subsLine = line.Split();
                        var beginDateFile = DateOnly.Parse(subsLine[0]);
                        var endDateFile = DateOnly.Parse(subsLine[1]);
                        if (beginDate >= beginDateFile && beginDate <= endDateFile && endDate >= beginDateFile && endDate <= endDateFile)
                        {
                            return false;
                        }
                        else if (beginDateFile >= beginDate && beginDateFile <= endDate || endDateFile >= endDate && endDateFile <= endDate)
                        {
                            return false;
                        }
                        line = reader.ReadLine();
                    }
                }
                return true;
            }
            else
            {
                throw new Exception("[Nie znaleziono pliku]");
            }
        }
    }
}
