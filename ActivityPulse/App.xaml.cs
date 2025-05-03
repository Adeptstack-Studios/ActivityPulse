using ActivityPulse.Models;
using ActivityPulse.Settings;
using System.Windows;
using System.Windows.Media;

namespace ActivityPulse
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CurrentUICulture;
        }

        public static CalendarThemeContext GetCalendarThemeColors()
        {
            CalendarThemeContext calendarTheme = new CalendarThemeContext();
            Color c = (Color)Current.Resources["AccentColor"];

            if (AppSettings.Default.ThemeMode == 0)
            {
                calendarTheme.ActiveColor = new SolidColorBrush(Color.FromArgb(160, c.R, c.G, c.B));
                calendarTheme.TodayColor = new SolidColorBrush(Color.FromArgb(255, c.R, c.G, c.B));
                calendarTheme.DisabledColor = new SolidColorBrush(Color.FromArgb(50, 0, 0, 0));
                calendarTheme.Foreground = Brushes.White;
                calendarTheme.DisabledForeground = Brushes.LightGray;
            }
            else if (AppSettings.Default.ThemeMode == 1)
            {
                calendarTheme.ActiveColor = new SolidColorBrush(Color.FromArgb(128, c.R, c.G, c.B));
                calendarTheme.TodayColor = new SolidColorBrush(Color.FromArgb(192, c.R, c.G, c.B));
                calendarTheme.DisabledColor = new SolidColorBrush(Color.FromArgb(110, 80, 80, 80));
                calendarTheme.Foreground = Brushes.White;
                calendarTheme.DisabledForeground = Brushes.LightGray;
            }
            else if (AppSettings.Default.ThemeMode == 2)
            {
                calendarTheme.ActiveColor = (SolidColorBrush)Current.Resources["ThemeAccentD"];
                calendarTheme.TodayColor = (SolidColorBrush)Current.Resources["ThemeMenu"];
                calendarTheme.DisabledColor = new SolidColorBrush(Color.FromArgb(150, ((SolidColorBrush)Application.Current.Resources["ThemeMenu"]).Color.R, ((SolidColorBrush)Application.Current.Resources["ThemeMenu"]).Color.G, ((SolidColorBrush)Application.Current.Resources["ThemeMenu"]).Color.B));
                calendarTheme.Foreground = Brushes.White;
                calendarTheme.DisabledForeground = Brushes.LightGray;
            }

            return calendarTheme;
        }
    }
    //calendarTheme.ActiveColor = new SolidColorBrush(Color.FromRgb(9, 132, 227));
    //calendarTheme.TodayColor = new SolidColorBrush(Color.FromRgb(6, 82, 221));
    //calendarTheme.ActiveColor = new SolidColorBrush(Color.FromRgb(52, 152, 219));
    //calendarTheme.TodayColor = new SolidColorBrush(Color.FromRgb(108, 92, 231));
}
