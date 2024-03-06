using JobSeniorityApp;

namespace JobSeniorityInFile.Tests
{
    public class Tests
    {
        [Test]
        public void CheckResultDifferentsDays()
        {
            // arrange
            var employee = new EmployeeInFile("Imie1", "Nazwisko1");
            employee.AddDuration("12.12.2000", "21.12.2000");

            //act
            employee.GetStatistics();
            var resultyears = employee.GetStatistics().Years;
            var resultmonths = employee.GetStatistics().Months;
            var resultdays = employee.GetStatistics().Days;

            //assert
            Assert.That(resultyears, Is.EqualTo(0));
            Assert.That(resultmonths, Is.EqualTo(0));
            Assert.That(resultdays, Is.EqualTo(10));
        }

        [Test]
        public void CheckResultDifferentsMonths()
        {
            // arrange
            var employee = new EmployeeInFile("Imie1", "Nazwisko1");
            employee.AddDuration("12.01.2000", "12.12.2000");

            //act
            employee.GetStatistics();
            var resultyears = employee.GetStatistics().Years;
            var resultmonths = employee.GetStatistics().Months;
            var resultdays = employee.GetStatistics().Days;

            //assert
            Assert.That(resultyears, Is.EqualTo(0));
            Assert.That(resultmonths, Is.EqualTo(11));
            Assert.That(resultdays, Is.EqualTo(1));
        }

        [Test]
        public void CheckResultIDifferentsYears()
        {
            // arrange
            var employee = new EmployeeInFile("Imie1", "Nazwisko1");
            employee.AddDuration("12.12.2000", "12.12.2001");

            //act
            employee.GetStatistics();
            var resultyears = employee.GetStatistics().Years;
            var resultmonths = employee.GetStatistics().Months;
            var resultdays = employee.GetStatistics().Days;

            //assert
            Assert.That(resultyears, Is.EqualTo(1));
            Assert.That(resultmonths, Is.EqualTo(0));
            Assert.That(resultdays, Is.EqualTo(1));
        }

        [Test]
        public void CheckResultMoreThanOneDuration()
        {
            // arrange
            var employee = new EmployeeInFile("Imie1", "Nazwisko1");
            employee.AddDuration("12.12.2000", "21.12.2000");
            employee.AddDuration("12.01.2001", "12.12.2001");
            employee.AddDuration("12.12.2002", "12.12.2003");

            //act
            employee.GetStatistics();
            var resultyears = employee.GetStatistics().Years;
            var resultmonths = employee.GetStatistics().Months;
            var resultdays = employee.GetStatistics().Days;

            //assert
            Assert.That(resultyears, Is.EqualTo(1));
            Assert.That(resultmonths, Is.EqualTo(11));
            Assert.That(resultdays, Is.EqualTo(12));
        }
    }
}

