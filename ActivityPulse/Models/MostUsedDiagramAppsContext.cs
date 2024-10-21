using System.Windows.Media;

namespace ActivityPulse.Models
{
    internal class MostUsedDiagramAppsContext
    {
        public string AppName { get; set; }
        public Brush Color { get; set; }
        public string IconPath { get; set; }

        public MostUsedDiagramAppsContext(string appName, Brush color, string iconPath)
        {
            this.AppName = appName;
            this.Color = color;
            this.IconPath = iconPath;
        }
    }
}
