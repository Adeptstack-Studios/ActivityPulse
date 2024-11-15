using System.Windows.Media;

namespace ActivityPulse.Models
{
    internal class MostUsedAppsContext
    {
        public string AppName { get; set; }
        public string UsedTime { get; set; }
        public string AVGUsedTime { get; set; }
        public string IconPath { get; set; }
        public Brush Color { get; set; }

        public MostUsedAppsContext(string appName, string usedTime, string iconPath, Brush color)
        {
            this.AppName = appName;
            this.UsedTime = usedTime;
            this.IconPath = iconPath;
            this.Color = color;
        }

        public MostUsedAppsContext(string appName, string usedTime, string avgUsedTime, string iconPath, Brush color)
        {
            this.AppName = appName;
            this.UsedTime = usedTime;
            this.AVGUsedTime = avgUsedTime;
            this.IconPath = iconPath;
            this.Color = color;
        }
    }
}
