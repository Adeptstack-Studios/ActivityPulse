using System.Globalization;

namespace ActivityUtils.Utils
{
    public class TimeUtils
    {
        public static bool Is24HForamt()
        {
            var dtf = CultureInfo.CurrentCulture.DateTimeFormat;
            string pattern = dtf.ShortTimePattern;

            bool is12Hour = pattern.Contains("tt");
            bool is24Hour = !is12Hour;

            return is24Hour;
        }

        public static bool IsValidTime(string input)
        {
            string[] formats = new[]
            {
                "H:mm",
                "HH:mm",
                "h:mm tt",
                "hh:mm tt"
            };

            return DateTime.TryParseExact(
                input,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _
            );
        }

        public static bool TryParseTimeToDateTime(string timeInput, DateTime baseDate, out DateTime result)
        {
            string[] formats = { "H:mm", "HH:mm", "h:mm tt", "hh:mm tt" };

            if (DateTime.TryParseExact(
                    timeInput,
                    formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedTime))
            {
                // Zeit übernehmen, Datum beibehalten
                result = new DateTime(
                    baseDate.Year,
                    baseDate.Month,
                    baseDate.Day,
                    parsedTime.Hour,
                    parsedTime.Minute,
                    0
                );
                return true;
            }

            result = default;
            return false;
        }
    }
}
