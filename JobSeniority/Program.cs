using JobSeniorityApp;

MainUserInterface();

void MainUserInterface()
{
    var endProgramm = false;
    while (!endProgramm)
    {
        Console.Clear();
        Console.WriteLine();
        Console.Title = "Staż pracy pracownika";
        Console.WriteLine($"\t       Witamy w programie obliczenia stażu pracy pracownika");
        Console.WriteLine($"\t=====================================================================\n");
        Console.WriteLine($"\t1   - Oblicz staż pracy\n");
        Console.WriteLine($"\t2   - 0blicz staż pracy, zapisz dane do pliku\n");
        Console.WriteLine($"\tQ/q - Wyjdż z programu\n");
        Console.WriteLine($"\t=====================================================================\n");
        Console.Write($"\tWybierz odpowiednią opcję: ");

        switch (Console.ReadLine())
        {
            case "1":
                AddEmployee("1");
                break;
            case "2":
                AddEmployee("2");
                break;
            case "Q":
            case "q":
                endProgramm = true;
                break;
            default:
                Console.Clear();
                break;
        }
    }
}

void AddEmployee(string menuOption)
{
    var endProgramm = false;
    while (!endProgramm)
    {
        GetDataFromUser(out string inputName, out string inputSurname, menuOption);

        if (IsInputStringValid(inputName) && IsInputStringValid(inputSurname))
        {
            ConversionStringFirstCapitalLetterOnly(ref inputName);
            ConversionStringFirstCapitalLetterOnly(ref inputSurname);

            switch (DisplaySelectionWithDataEmployee(inputName, inputSurname))
            {
                case "Y":
                case "y":
                    try
                    {
                        EmployeeBase employee = menuOption == "1" ?
                            new EmployeeInMemory(inputName, inputSurname) :
                            new EmployeeInFile(inputName, inputSurname);
                        AddDurationsAndShowResult(employee);
                    }
                    catch (Exception exception)
                    {
                        DisplayExceptionMessage(exception.Message);
                    }
                    finally
                    {
                        WaitForKeyPress();
                    }
                    break;
                case "N":
                case "n":
                    break;
                default:
                    endProgramm = true;
                    break;
            }
        }
        else
        {
            switch (DisplaySelectionWithInvalidDataEmployee())
            {
                case "Q":
                case "q":
                    endProgramm = true;
                    break;
                default:
                    break;
            }
        }
    }
}

void AddDurationsAndShowResult(EmployeeBase employee)
{
    AddDuration(employee);
    employee.ShowStatistics();
}

void AddDuration(EmployeeBase employee)
{
    var index = 1;
    var endMethod = false;

    while (!endMethod)
    {
        Console.Clear();
        Console.WriteLine($"\t   Wprowadzanie okresów pracy dla {employee.Name} {employee.Surname}");
        Console.WriteLine($"\t===========================================================================\n");
        employee.ShowDurations();
        try
        {
            Console.Write($"\tPodaj datę poczatkową \t{index}. okresu pracy (dd.mm.rrrr): \t");
            var begindate = Console.ReadLine();
            Console.Write($"\tPodaj datę końcową \t{index}. okresu pracy (dd.mm.rrrr): \t");
            var endDate = Console.ReadLine();

            employee.DurationAdded += EmployeeDurationAdded;
            employee.AddDuration(begindate, endDate);
        }
        catch (Exception exception)
        {
            DisplayExceptionMessage(exception.Message);
            index--;
        }
        finally
        {
            employee.DurationAdded -= EmployeeDurationAdded;
            Console.WriteLine();
        }

        Console.Write($"\n\t(N/n- wprowadź następny, Dowolny klawisz- wyniki pracownika): ");
        switch (Console.ReadLine())
        {
            case "N":
            case "n":
                index++;
                break;
            default:
                endMethod = true;
                break;
        }
    }
}

void GetDataFromUser(out string inputName, out string inputSurname, string menuOption)
{
    Console.Clear();
    switch (menuOption)
    {
        case "1":
            Console.WriteLine($"\t\t\t Wprowadzanie danych pracownika");
            break;
        case "2":
            Console.WriteLine($"\t     Wprowadzanie danych pracownika, zapis danych do pliku ");
            break;
        default:
            break;
    }
    Console.WriteLine($"\t=====================================================================\n");
    Console.Write($"\tPodaj imię pracownika:\t\t");
    inputName = Console.ReadLine();
    Console.Write($"\tPodaj nazwisko pracownika:\t");
    inputSurname = Console.ReadLine();
}

void ConversionStringFirstCapitalLetterOnly(ref string inputstring)

{
    inputstring = char.ToUpper(inputstring[0]) + inputstring[1..].ToLower();
}

string DisplaySelectionWithDataEmployee(string inputName, string inputSurname)
{
    Console.WriteLine($"\n\tDane pracownika: {inputName} {inputSurname}");
    Console.Write($"\n\n\t(Y/y - potwierdź, N/n- wprowadzanie nowego, Wyjście- dowolny klawisz): ");
    return Console.ReadLine();
}

string DisplaySelectionWithInvalidDataEmployee()
{
    Console.Write($"\n\tNiewłaściwe dane (Ponowne wprowadzenie- dowolny klawisz, Q/q- rezygnacja): ");
    return Console.ReadLine();
}

bool IsStringWithPolishLettersOnly(string inputstring)
{
    foreach (char c in inputstring)
    {
        if (!((c >= 65 && c <= 90) || (c >= 97 && c <= 122) || (c >= 260 && c <= 263) || (c >= 321 && c <= 324) ||

            (c >= 377 && c <= 380) || (c == 211) || (c >= 280 && c <= 281) || (c >= 346 && c <= 347) || (c == 243)))
        {
            return false;
        }
    }
    return true;
}

bool IsInputStringValid(string inputstring)
{
    return IsStringWithPolishLettersOnly(inputstring) && !string.IsNullOrEmpty(inputstring);
}

void EmployeeDurationAdded(object sender, EventArgs args)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"\n\tDodano nowy okres pracy");
    Console.ResetColor();
    WaitForKeyPress();
}

void DisplayExceptionMessage(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\t\n {message}");
    Console.ResetColor();
}

void WaitForKeyPress()
{
    Console.Write($"\n\tNaciśnij dowolny klawisz ");
    Console.ReadKey();
}
