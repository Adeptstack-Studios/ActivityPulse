using ActivityPulse.Models;
using System.Diagnostics;
using System.Windows;

namespace ActivityPulse.Utils
{
    public class AppUtils
    {
        public static string apVersion = "1.0.0";
        public static string ahVersion = "1.0.0";
        public static string apServerVersion = "1.0.0";
        public static string ahServerVersion = "1.0.0";

        public static VersionInformationContext latestVersion = new VersionInformationContext();

        public static void Restart()
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Application.Current.Shutdown();
        }
    }
}
