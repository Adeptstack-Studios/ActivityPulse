using System.Windows.Media;

namespace ActivityPulse.Models
{
    internal class Day
    {
        public string DayOfWeek { get; set; }
        public int DayInMonth { get; set; }
        public Brush BgBrush { get; set; }
        public Brush FgBrush { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime DateTime { get; set; }
    }
}
