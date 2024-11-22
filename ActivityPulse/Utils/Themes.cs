using ActivityPulse.Settings;
using PLP_SystemInfo;

namespace ActivityPulse.Utils
{
    class Themes
    {
        public static void SetMode(int selectionIndex)
        {
            switch (selectionIndex)
            {
                case 0:
                    AppSettings.Default.ThemeMode = !SystemInfo.IsDarkModeEnabled ? 1 : 0;
                    AppSettings.Default.Save();
                    break;
                case 1:
                    AppSettings.Default.ThemeMode = 0;
                    AppSettings.Default.Save();
                    break;
                case 2:
                    AppSettings.Default.ThemeMode = 1;
                    AppSettings.Default.Save();
                    break;
                default:
                    break;
            }
        }
    }
}
