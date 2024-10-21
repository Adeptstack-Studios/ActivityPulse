using ActivityPulse.Models;
using ActivityPulse.Windows;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace ActivityPulse.Pages
{
    /// <summary>
    /// Interaktionslogik für CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        List<Day> dayList = new List<Day>();
        Month months = new Month(DateTime.Now.Year, DateTime.Now.Month);
        public CalendarPage()
        {
            InitializeComponent();
            MainWindow.UpdateTitle("ActivityPulse - Home");
            CLD(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void CLD(int year, int month)
        {
            months = new Month(year, month);

            TB_YearMonth.Text = months.ToString();
            var firstDay = months.Days.First();
            var daysBeforeMonth = ((int)firstDay.DayOfWeek + 6) % 7;
            var dayNames = CultureInfo.CurrentUICulture.DateTimeFormat.DayNames;

            dayNames = dayNames
                .Skip(1)
                .Concat(dayNames.Take(1))
                .ToArray();

            dayName0.Text = dayNames[0].ToString();
            dayName1.Text = dayNames[1].ToString();
            dayName2.Text = dayNames[2].ToString();
            dayName3.Text = dayNames[3].ToString();
            dayName4.Text = dayNames[4].ToString();
            dayName5.Text = dayNames[5].ToString();
            dayName6.Text = dayNames[6].ToString();

            dayList.Clear();
            CalendarThemeContext calendarTheme = App.GetCalendarThemeColors();
            for (var i = -daysBeforeMonth; i < 42 - daysBeforeMonth; i++)
            {
                if (firstDay.AddDays(i).Date.Month == months.MonthOfYear)
                {
                    if (firstDay.AddDays(i).Date.ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        dayList.Add(new Day
                        {
                            DayInMonth = firstDay.AddDays(i).Day,
                            DayOfWeek = firstDay.AddDays(i).DayOfWeek.ToString(),
                            BgBrush = calendarTheme.TodayColor,
                            FgBrush = calendarTheme.Foreground,
                            DayBrush = calendarTheme.ActiveColor,
                            IsEnabled = true,
                            DateTime = firstDay.AddDays(i).Date,
                            TimeUsed = TodayPage.GetTimeString(TodayPage.GetGeneralData(firstDay.AddDays(i).Date).gesSecondsUsed),
                        });
                    }
                    else
                    {
                        dayList.Add(new Day
                        {
                            DayInMonth = firstDay.AddDays(i).Day,
                            DayOfWeek = firstDay.AddDays(i).DayOfWeek.ToString(),
                            BgBrush = calendarTheme.ActiveColor,
                            FgBrush = calendarTheme.Foreground,
                            DayBrush = calendarTheme.TodayColor,
                            IsEnabled = true,
                            DateTime = firstDay.AddDays(i).Date,
                            TimeUsed = firstDay.AddDays(i).Date < DateTime.Now ? TodayPage.GetTimeString(TodayPage.GetGeneralData(firstDay.AddDays(i).Date).gesSecondsUsed) : "",
                        });
                    }
                }
                else
                {
                    dayList.Add(new Day
                    {
                        DayInMonth = firstDay.AddDays(i).Day,
                        DayOfWeek = firstDay.AddDays(i).DayOfWeek.ToString(),
                        BgBrush = calendarTheme.DisabledColor,
                        FgBrush = calendarTheme.DisabledForeground,
                        DayBrush = calendarTheme.DisabledColor,
                        IsEnabled = false,
                        DateTime = firstDay.AddDays(i).Date,
                    });
                }
            }

            LBDays.ItemsSource = null;
            LBDays.ItemsSource = dayList;
        }

        private void previosMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CLD(months.PreviousMonth.Year, months.PreviousMonth.MonthOfYear);
            }
            catch (Exception ex)
            {
                TellBox tellBox = new TellBox(ex.Message, "Error");
            }
        }

        private void nextMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CLD(months.NextMonth.Year, months.NextMonth.MonthOfYear);
            }
            catch (Exception ex)
            {
                TellBox tellBox = new TellBox(ex.Message, "Error");
            }
        }

        private void currentMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            CLD(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void jumpToMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            jumpToDialog.Visibility = System.Windows.Visibility.Visible;
        }

        private void closeJumpToDialog_Click(object sender, RoutedEventArgs e)
        {
            jumpToDialog.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void JumpToBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int month = Convert.ToInt32(TBMonth.Text);
                int year = Convert.ToInt32(TBYear.Text);
                if (year > 0 && month > 0 && month <= 12)
                {
                    CLD(year, month);
                    jumpToDialog.Visibility = System.Windows.Visibility.Collapsed;
                    TBMonth.Text = string.Empty;
                    TBYear.Text = string.Empty;
                }
                else
                {
                    TellBox tellBox = new TellBox("You made an incorrect entry.", "Error");
                }
            }
            catch (Exception ex)
            {
                TellBox tellBox = new TellBox(ex.Message, "Error");
            }
        }

        private void LBDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBDays.SelectedIndex >= 0)
            {
                NavigationService.Content = new TodayPage(dayList[LBDays.SelectedIndex].DateTime);
            }
        }

        void UpdateCalendar()
        {
            CLD(months.Year, months.MonthOfYear);
        }
    }
}
