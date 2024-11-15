using System.Windows.Media;

namespace ActivityPulse.Models
{
    class IntervallOverviewContext
    {
        public DateTime DateTime { get; set; }
        public string UsedTime { get; set; }
        public double BalkenWidth { get; set; }
        public Brush BdrBrush { get; set; }
        public string DateString
        {
            get => DateTime.ToShortDateString();
        }

        public IntervallOverviewContext(DateTime dateTime, string usedTime, double balkenWidth, Brush bdrBrush)
        {
            DateTime = dateTime;
            UsedTime = usedTime;
            BalkenWidth = balkenWidth;
            BdrBrush = bdrBrush;
        }
    }
}
