using System;
using System.Globalization;
using TimeSheet.src.util;

namespace TimeSheet.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var culture = CultureInfo.CurrentCulture;
            var totalTime = new TimeSpan(08,00,00);
            bool result;
            TimeSpan timeIn1TimeSpan, timeOut1TimeSpan, timeIn2TimeSpan;

            Console.WriteLine("TimeSheet");
            Console.WriteLine();
            do {
                Console.WriteLine("Check In (format 14:14)");
                var timeIn1 = Console.ReadLine();
                result = Time.ParseTimeSpan(timeIn1, out timeIn1TimeSpan);
                if (!result) {
                    Console.WriteLine("Error reading time");
                }
            } while(!result);
            do {
                Console.WriteLine("Check Out (format 14:14)");
                var timeOut1 = Console.ReadLine();
                result = Time.ParseTimeSpan(timeOut1, out timeOut1TimeSpan);
                if (!result) {
                    Console.WriteLine("Error reading time");
                }
            } while(!result);
            do {
                Console.WriteLine("Check In (format 14:14)");
                var timeIn2 = Console.ReadLine();
                result = Time.ParseTimeSpan(timeIn2, out timeIn2TimeSpan);
                if (!result) {
                    Console.WriteLine("Error reading time");
                }
            } while (!result);
            // show expected time out
            var time2leave = timeIn1TimeSpan.Add(totalTime).Add(timeIn2TimeSpan.Subtract(timeOut1TimeSpan));
            Console.WriteLine($"Time to leave: {time2leave}");
        }
    }
}
