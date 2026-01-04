using ActivityPulse.Models;
using ActivityPulse.Utils;
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

        //Jump To
        private void jumpToMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            dateControl.Open();
        }

        private void dateControl_OnDateDialogClick(DateTime date)
        {
            CLD(date.Year, date.Month);
        }

        //Navigate to Dayspage
        private void LBDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LBDays.SelectedIndex >= 0)
            {
                NavigationService.Content = new TodayPage(dayList[LBDays.SelectedIndex].DateTime);
            }
        }

        //Summary Pages
        private void avgWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            summaryDateWeek.Open();
        }

        private void avgMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            summaryDateMonth.Open();
        }

        private void avgYearBtn_Click(object sender, RoutedEventArgs e)
        {
            summaryDateYear.Open();
        }

        List<DateTime> GetWeekInterval(DateTime d)
        {
            DayOfWeek firstDayOfWeek = CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek;
            List<DateTime> dates = new List<DateTime>();
            DayOfWeek dayOfWeek = d.DayOfWeek;
            int firstDayIndex = (int)firstDayOfWeek;
            int chosenDayIndex = (int)dayOfWeek;
            int dayIndex = (chosenDayIndex - firstDayIndex + 7) % 7;

            d = d.AddDays(-dayIndex).Date;

            for (int i = 0; i < 7; i++)
            {
                dates.Add(d.AddDays(i));
            }

            return dates;
        }

        private void summaryDateWeek_OnDateDialogClick(DateTime date)
        {
            NavigationService.Content = new AveragePage(GetWeekInterval(date), AverageType.Week);
        }

        private void summaryDateMonth_OnDateDialogClick(DateTime date)
        {
            DateTime d = new DateTime(date.Year, date.Month, 1);
            List<DateTime> dates = new List<DateTime>();

            int day = 0;
            do
            {
                dates.Add(d.AddDays(day));
                day++;
            } while (d.AddDays(day).Month == date.Month && d.AddDays(day) <= DateTime.Now);

            NavigationService.Content = new AveragePage(dates, AverageType.Month);
        }

        private void summaryDateYear_OnDateDialogClick(DateTime date)
        {
            DateTime d = new DateTime(date.Year, 1, 1);
            List<DateTime> dates = new List<DateTime>();

            int day = 0;
            do
            {
                dates.Add(d.AddDays(day));
                day++;
            } while (d.AddDays(day).Year == date.Year && d.AddDays(day) <= DateTime.Now);

            NavigationService.Content = new AveragePage(dates, AverageType.Year);
        }
    }
}
