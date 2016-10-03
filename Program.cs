using System;
using System.Globalization;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var culture = CultureInfo.CurrentCulture;
            var totalTime = new TimeSpan(08,00,00);

            Console.WriteLine("TimeSheet");
            Console.WriteLine();
            Console.WriteLine("Check In (format 14:14)");
            var timeIn1 = Console.ReadLine();
            var timeIn1Date = TimeSpan.ParseExact(timeIn1, "c", culture);
            Console.WriteLine("Check Out (format 14:14)");
            var timeOut1 = Console.ReadLine();
            var timeOut1Date = TimeSpan.ParseExact(timeOut1, "c", culture);
            Console.WriteLine("Check In (format 14:14)");
            var timeIn2 = Console.ReadLine();
            var timeIn2Date = TimeSpan.ParseExact(timeIn2, "c", culture);
            // show expected time out
            var time2leave = timeIn1Date.Add(totalTime).Add(timeIn2Date.Subtract(timeOut1Date));
            Console.WriteLine($"Time to leave: {time2leave}");
        }
    }
}
