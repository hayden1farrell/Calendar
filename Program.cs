using System.Transactions;

namespace Calendar;
using System;
class Calendar
{
    private int _startDay = 1;      // start of UTC
    private int _startMonth = 0;
    private int _startYear = 1970;
    
    private int _currentDay;    // day of code executed
    private int _currentMonth;
    private int _currentYear;
    public int CurrentDay { get { return _currentDay; } set => _currentDay = value; }
    public int CurrentMonth { get { return _currentMonth; } set => _currentMonth = value; }
    public int CurrentYear { get { return _currentDay; } set => _currentYear = value; }

    private string[,] _calendar;
    public Calendar()
    {
        SetStart();
        GetCurrentMonth();
        DisplayCalender();
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
        _currentDay = dateTime.Day;
        _currentMonth = dateTime.Month;
        _currentYear = dateTime.Year;
    }
    private void NextMonth()
    {
        
    }

    private void LastMonth()
    {
        
    }

    private void DisplayCalender()
    {
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
    }
}