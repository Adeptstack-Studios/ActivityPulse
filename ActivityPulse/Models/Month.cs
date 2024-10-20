using System.Globalization;

namespace ActivityPulse.Models
{
    public record Month(int Year, int MonthOfYear)
    {
        public int CountOfDays { get; } = DateTime.DaysInMonth(Year, MonthOfYear);

        public IEnumerable<DateTime> Days => Enumerable.Range(1, CountOfDays)
            .Select(day => new DateTime(Year, MonthOfYear, day));

        public override string ToString() => Days.First().ToString("MMMM yyyy", CultureInfo.CurrentUICulture);

        public Month NextMonth => MonthOfYear == 12
            ? new(Year + 1, 1)
            : new(Year, MonthOfYear + 1);

        public Month PreviousMonth => MonthOfYear == 1
            ? new(Year - 1, 12)
            : new(Year, MonthOfYear - 1);
    }
}
