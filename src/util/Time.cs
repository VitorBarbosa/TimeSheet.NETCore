using System;
using System.Globalization;

namespace TimeSheet.src.util {
	public class Time {
		/// <summary>
		/// Parses a string into a TimeSpan with the desired format string
		/// </summary>
		/// <param name="timeSpanString"></param>
		/// <param name="timeSpan"></param>
		/// <param name="format"></param>
		/// <returns>True if string is able to be parsed into a TimeSpan</returns>
		public static bool ParseTimeSpan(string timeSpanString, out TimeSpan timeSpan, string format = "c") {
            var cultureInfo = CultureInfo.CurrentCulture;

            if (TimeSpan.TryParseExact(timeSpanString, format, cultureInfo, out timeSpan)) {
                return true;
            }

            return false;
        }

		public static TimeSpan CalculateResultingTimeSpan(params TimeSpan[] timeSpans) {
			if (timeSpans.Length % 2 != 0) {
				throw new ArgumentException("Odd number of timeSpans");
			}

			var result = new TimeSpan();

			for (var i = 0; i < timeSpans.Length; i += 2) {
				result = timeSpans[i+1].Subtract(timeSpans[i]);
			}

			return result;
		}
	}
}