namespace ActivityUtilities.Models
{
    public class AppUsage
    {
        public string AppName { get; set; } = "";
        public int UsedMinutes { get; set; } = 0;
        public int UsedSeconds { get; set; } = 0;
        public List<DateTime> timeUsed { get; set; } = new List<DateTime>();
        public string IconPath { get; set; } = "";
    }
}
