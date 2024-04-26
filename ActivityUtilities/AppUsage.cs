namespace ActivityUtilities
{
    public class AppUsage
    {
        public string AppName { get; set; } = "";
        public int UsedMinutes { get; set; }
        public int UsedSeconds { get; set; }
        public List<DateTime> timeUsed { get; set; } = new List<DateTime>();
    }
}
