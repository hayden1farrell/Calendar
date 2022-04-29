using System.Diagnostics;
using System.Transactions;

namespace Calendar;
using System;
class Calendar
{
    private int _startDay = 1;      // start of UTC
    private int _startMonth = 0;
    private int _startYear = 1970;
    private int _finalDay = 5;      // Mon = 0 Sun = 6
    private Dictionary<int, int> _monthToDays = new()
    { {0, 31},{1, 28},{2, 31},{3, 30},{4, 31},{5, 30},{6, 31},{7, 31},{8, 30},{9, 31},{10, 30},{11, 31}};

    private int _currentDay = 1;        // day of code executed
    private int _currentMonth = 0;
    private int _currentYear = 1970;
    
    private int _todayDay;        // day of code executed
    private int _todayMonth;
    private int _todayYear;
    public int CurrentDay { get { return _todayDay; } set => _todayDay = value; }
    public int CurrentMonth { get { return _todayMonth; } set => _todayMonth = value; }
    public int CurrentYear { get { return _todayYear; } set => _todayYear = value; }

    private string[,] _calendar;
    public Calendar()
    {
        SetStart();
        GetCurrentMonth();

        while((_currentMonth + 1 != _todayMonth) || (_currentYear != _todayYear))
            NextMonth();    //Gets to the user current month
        DisplayCalender();
    }

    public void GetUserSelectedDate()
    {
        
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
        for (i = 0; i < _finalDay; i++)
            _calendar[0, i] = "  ";

        int day = 0;
        for (int temp = i; temp < 7; temp++)
        {
            day++;
            _calendar[0,temp] = $"{day:00}";
        }

        return day;
    }
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
        if ((_currentMonth == 0 && offset == 1) || (_currentMonth == 11 && offset == -1))
            _currentYear += offset;
    }

    private void LastMonth()
    {
        
    }

    private void DisplayCalender()
    {
        Console.WriteLine($"MONTH: {_currentMonth + 1}, Year: {_currentYear}");
        Console.WriteLine("Mo  Tu  We  Th  Fr  Sa  Su");
        for (int y = 0; y < _calendar.GetLength(0); y++)
        {
            for (int x = 0; x < _calendar.GetLength(1); x++)
            {
                if (x > 4) Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(_calendar[y,x] + "  ");
                Console.ForegroundColor = ConsoleColor.Black;
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
        Console.WriteLine("START");
        Calendar calendar = new Calendar();
        calendar.GetUserSelectedDate();
    }
}