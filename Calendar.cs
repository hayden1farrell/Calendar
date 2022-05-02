namespace Calendar;
using System;
class Calendar
{
    private int _finalDay = 5;      // Mon = 0 Sun = 6
    private int _firstDay = 3;
    private Dictionary<int, int> _monthToDays = new()
    { {0, 31},{1, 28},{2, 31},{3, 30},{4, 31},{5, 30},{6, 31},{7, 31},{8, 30},{9, 31},{10, 30},{11, 31}};
    private Dictionary<int, string> _numberToName = new()
        { {1, "January"},{2, "Febuary"},{3, "March"},{4, "April"},{5, "May"},{6, "June"},{7, "July"},{8, "August"},{9, "September"},{10, "October"}
            ,{11, "November"},{12, "December"}};
    private int _currentMonth = 0;
    private int _currentYear = 1970;
    
    private int _todayDay;        // day of code executed
    private int _todayMonth;
    private int _todayYear;

    private int _selectedDay;
    private int _selctedMonth;
    private int _selectedYear;

    private string[,] _calendar;
    public Calendar()
    {
        SetStart();
        GetCurrentMonth();

        while((_currentMonth + 1 != _todayMonth) || (_currentYear != _todayYear))
            NextMonth();    //Gets to the user current month
    }

    public void GetUserSelectedDate()
    {
        DisplayCalender();
        Console.WriteLine("<- select day ->");
        Console.WriteLine("Please enter the number you want last or next");
        string? choice = Console.ReadLine()?.ToLower();
        if(choice == "next")
            NextMonth();
        else if(choice == "last")
            LastMonth();
        else
        {
            int day = GetIntInput(choice);
            if (day == -1) GetUserSelectedDate();
            int numberOfDays = _monthToDays[_currentMonth];
            if(_currentMonth == 1)
                if (IsLeapYear() == true)
                    numberOfDays++;
            
            if (Convert.ToInt32(choice) < 1 || Convert.ToInt32(choice) > numberOfDays){
                Console.WriteLine("Invalid day");
                GetUserSelectedDate();
            }
            DaySelected(day);
            return;
        }
        GetUserSelectedDate();
    }

    private void DaySelected(int choice)
    {
        _selectedDay = choice;
        _selctedMonth = _currentMonth + 1;
        _selectedYear = _currentYear;
    }

    public void DisplayUserSelectedDate()
    {
        Console.WriteLine($"Selected day: {_selectedDay} selected month: {_numberToName[_selctedMonth]} YEAR: {_selectedYear}");

    }
    private int GetIntInput(string? choice)
    {
        try
        {
            if (choice != null) return int.Parse(choice);
        }
        catch (Exception e)
        {
            return -1;
        }

        return -1;
    }

    private bool IsLeapYear()
    {
        if ((_currentYear % 4 == 0) && (_currentYear % 100 != 0) || (_currentYear % 400 == 0))
            return true;
        return false;
    }

    private void SetStart()
    {
        string[,] temp = {
            {"  ", "  ", "  ", "01", "02", "03", "04"},
            {"05", "06", "07", "08", "09", "10", "11"},
            {"12", "13", "14", "15", "16", "17", "18"},
            {"19", "20", "21", "22", "23", "24", "25"},
            {"26", "27", "28", "29", "30", "31", "  "},
            {"  ", "  ", "  ", "  ", "  ", "  ", "  "}};
        _calendar = temp;
    }

    private void GetCurrentMonth()
    {
        var dateTime = DateTime.Today;
        _todayDay = dateTime.Day;
        _todayMonth = dateTime.Month;
        _todayYear = dateTime.Year;
    }

    private int NewMonth(int increment)
    {
        Increment(increment);
        Array.Clear(_calendar, 0, _calendar.Length);
        _finalDay = (_finalDay + increment) % 7;
        int i = 0;
        for (; i < _finalDay; i++)
            _calendar[0, i] = "  ";

        int day = 0;
        _firstDay = i;
        for (int temp = i; temp < 7; temp++)
        {
            day++;
            _calendar[0,temp] = $"{day:00}";
        }

        return day;
    } // NEED TO CLEAN || HURTS TO LOOK AT
    private void NextMonth()
    {
        int day = NewMonth(1);
        int numberOfDays = _monthToDays[_currentMonth];
        if(_currentMonth == 1)
            if (IsLeapYear() == true)
                numberOfDays++;
        
        int counter = 7;
        for (;day < numberOfDays; counter++)
        {
            day++;
            _calendar[counter/7, counter % 7] = $"{day:00}";
        }
        _finalDay = (counter-1) % 7;
    }

    private void Increment(int offset)
    {
        _currentMonth = (_currentMonth + offset) % 12;
        if ((_currentMonth == 0 && offset == 1))
            _currentYear += offset;
        if (_currentMonth == -1)
        {
            _currentMonth = 11;
            _currentYear -= 1;
        }
    }

    private void LastMonth()
    {
        Increment(-1);
        FillArray();
        _finalDay = (_finalDay - 1) % 7;
        
        int numberOfDays = _monthToDays[_currentMonth];
        if(_currentMonth == 1)
            if (IsLeapYear())
                numberOfDays++;
        
        int start = 7 - System.Math.Abs((_firstDay - numberOfDays) % 7);
        
        int i = 0;
        for (; i < start; i++)
            _calendar[0, i] = "  ";

        int day = 0;
        _firstDay = i;
        for (int temp = i; temp < 7; temp++)
        {
            day++;
            _calendar[0,temp] = $"{day:00}";
        }
        int counter = 7;
        for (;day < numberOfDays; counter++)
        {
            day++;
            _calendar[counter/7, counter % 7] = $"{day:00}";
        }
        _finalDay = (counter-1) % 7;
    }   // NEED TO CLEAN || TO UGLY TO LOOK AT

    private void FillArray()
    {
        for (int y = 0; y < _calendar.GetLength(0); y++)
        {
            for (int x = 0; x < _calendar.GetLength(1); x++)
            {
                _calendar[y, x] = "  ";
            }
        }
    }

    private void DisplayCalender()
    {
        Console.Clear();
        Console.WriteLine($"MONTH: {_numberToName[_currentMonth + 1]}, Year: {_currentYear}");
        Console.WriteLine("Mo  Tu  We  Th  Fr  Sa  Su");
        for (int y = 0; y < _calendar.GetLength(0); y++)
        {
            for (int x = 0; x < _calendar.GetLength(1); x++)
            {
                if (x > 4) Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(_calendar[y,x] + "  ");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine("");
        }
        
    }

    private void ConvertToDateTime()
    {
        
    }
}

class Program
{
    static void Main()
    {
        Calendar calendar = new Calendar();
        calendar.GetUserSelectedDate();
        calendar.DisplayUserSelectedDate();
    }
}