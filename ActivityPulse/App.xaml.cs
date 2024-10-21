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
        public static CalendarThemeContext GetCalendarThemeColors()
        {
            CalendarThemeContext calendarTheme = new CalendarThemeContext();

            if (AppSettings.Default.ThemeMode == 0)
            {
                calendarTheme.ActiveColor = new SolidColorBrush(Color.FromRgb(52, 152, 219));
                calendarTheme.TodayColor = new SolidColorBrush(Color.FromRgb(108, 92, 231));
                calendarTheme.DisabledColor = new SolidColorBrush(Color.FromArgb(90, 0, 0, 0));
                calendarTheme.Foreground = Brushes.White;
                calendarTheme.DisabledForeground = Brushes.LightGray;
            }
            else if (AppSettings.Default.ThemeMode == 1)
            {
                calendarTheme.ActiveColor = new SolidColorBrush(Color.FromRgb(9, 132, 227));
                calendarTheme.TodayColor = new SolidColorBrush(Color.FromRgb(6, 82, 221));
                calendarTheme.DisabledColor = new SolidColorBrush(Color.FromArgb(110, 80, 80, 80));
                calendarTheme.Foreground = Brushes.White;
                calendarTheme.DisabledForeground = Brushes.LightGray;
            }
            else if (AppSettings.Default.ThemeMode == 2)
            {
                calendarTheme.ActiveColor = (SolidColorBrush)Application.Current.Resources["ThemeAccentD"];
                calendarTheme.TodayColor = (SolidColorBrush)Application.Current.Resources["ThemeMenu"];
                calendarTheme.DisabledColor = new SolidColorBrush(Color.FromArgb(150, ((SolidColorBrush)Application.Current.Resources["ThemeMenu"]).Color.R, ((SolidColorBrush)Application.Current.Resources["ThemeMenu"]).Color.G, ((SolidColorBrush)Application.Current.Resources["ThemeMenu"]).Color.B));
                calendarTheme.Foreground = Brushes.White;
                calendarTheme.DisabledForeground = Brushes.LightGray;
            }

            return calendarTheme;
        }
    }

}
