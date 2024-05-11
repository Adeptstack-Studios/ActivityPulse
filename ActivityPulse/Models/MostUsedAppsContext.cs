namespace ActivityPulse.Models
{
    internal class MostUsedAppsContext
    {
        public string AppName { get; set; }
        public string UsedTime { get; set; }
        public string IconPath { get; set; }

        public MostUsedAppsContext(string appName, string usedTime, string iconPath)
        {
            this.AppName = appName;
            this.UsedTime = usedTime;
            this.IconPath = iconPath;
        }
    }
}
