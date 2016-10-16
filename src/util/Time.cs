using System;
using System.Collections.Generic;
using System.Globalization;

namespace TimeSheet.src.util {
	public class Time {
		public IList<TimeSpan> TimesIn { get; set; }
		public IList<TimeSpan> TimesOut { get; set; }

		public Time (){
		  TimesIn = new List<TimeSpan>();
		  TimesOut = new List<TimeSpan>();
		}

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

		public TimeSpan CalculateTimeOut() {
			var totalTime = new TimeSpan(08, 00, 00);
			var totalTimeOut = new TimeSpan(0, 0, 0);
			for (var i = 1; i < TimesIn.Count; i++) {
				totalTimeOut = totalTimeOut.Add(TimesIn[i].Subtract(TimesOut[i - 1]));
			}
			return TimesIn[0].Add(totalTime).Add(totalTimeOut);
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