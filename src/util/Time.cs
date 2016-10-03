using System;
using System.Globalization;

namespace TimeSheet.src.util {
	public class Time {
		public static bool ParseTimeSpan(string timeSpanString, out TimeSpan timeSpan, string format = "c") {
            var cultureInfo = CultureInfo.CurrentCulture;

            if (TimeSpan.TryParseExact(timeSpanString, format, cultureInfo, out timeSpan)) {
                return true;
            }

            return false;
        }
	}
}