using ActivityPulse.Models;
using ActivityPulse.Utils;
using ActivityUtilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ActivityPulse.Pages
{
    /// <summary>
    /// Interaktionslogik für TodayPage.xaml
    /// </summary>
    public partial class TodayPage : Page
    {
        GeneralData gData;
        List<AppUsage> appUsages;
        DateTime day;

        public TodayPage(DateTime day)
        {
            InitializeComponent();
            this.day = day;
            MainWindow.UpdateTitle($"ActivityPulse - {day.ToShortDateString()}");

            tbkTitle.Text = day.ToShortDateString();
            gData = GetGeneralData(day);
            appUsages = GetAppUsages(day);

            tbkScreenTime.Text = GetTimeString(gData.gesSecondsUsed);
        }

        public static GeneralData GetGeneralData(DateTime day)
        {
            string storeFolder = @$"{Data.path}{day.Year}/{day.Month}/{day.Day}";
            string storeFileGeneral = @$"{Data.path}{day.Year}/{day.Month}/{day.Day}/General.json";

            return Data.LoadGeneralData(storeFileGeneral);
        }

        public static List<AppUsage> GetAppUsages(DateTime day)
        {
            string storeFolder = $@"{Data.path}{day.Year}/{day.Month}/{day.Day}";
            string storeFileApps = @$"{Data.path}{day.Year}/{day.Month}/{day.Day}/{day.Day}.json";

            return Data.LoadList(storeFileApps);
        }

        public static string GetTimeString(long seconds)
        {
            long gesMins = seconds / 60;
            long hour = gesMins / 60;
            long minutes = gesMins % 60;

            if (hour == 0)
            {
                return $"{minutes} m";
            }
            return $"{hour} h  {minutes} m";
        }

        void CreateLineDiagramMostUsedApps(GeneralData gData, List<AppUsage> appUsages)
        {
            canvasMostUsedApps.Children.Clear();
            List<AppUsage> mostUsed = appUsages.OrderByDescending(t => t.UsedMinutes).ThenByDescending(t => t.UsedSeconds).ToList(); //from t in appUsages orderby t.UsedMinutes, t.UsedSeconds select new { t.AppName, t.UsedMinutes, t.UsedSeconds, t.timeUsed, t.IconPath };

            double canavsWidth = canvasMostUsedApps.ActualWidth;
            double gesUsedTime = gData.gesSecondsUsed;
            double marginLeft = 0;

            for (int i = 0; i < (mostUsed.Count >= 3 ? 3 : mostUsed.Count); i++)
            {
                double gesAppTime = (mostUsed[i].UsedMinutes * 60) + mostUsed[i].UsedSeconds;
                double percentige = gesAppTime / gesUsedTime;

                Rectangle rect = new Rectangle
                {
                    Height = 40,
                    Width = canavsWidth * percentige,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorLists.colors[i])),
                    StrokeThickness = 0,
                };

                if (i == 0)
                {
                    rect.RadiusX = 10;
                    rect.RadiusY = 10;

                    Rectangle filler = new Rectangle
                    {
                        Width = 10,
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorLists.colors[i])),
                        StrokeThickness = 0,
                        Height = 40,
                    };

                    canvasMostUsedApps.Children.Add(filler);
                    Canvas.SetLeft(filler, (canavsWidth * percentige) - 10);
                    Canvas.SetTop(filler, 0);
                }

                canvasMostUsedApps.Children.Add(rect);
                Canvas.SetLeft(rect, marginLeft);
                Canvas.SetTop(rect, 0);
                marginLeft += rect.Width - 1;
            }
            LastRect();
            ListApps();
            ListOther();

            void LastRect()
            {
                Rectangle otherRect = new Rectangle
                {
                    Height = 40,
                    Width = canavsWidth - marginLeft,
                    Fill = Brushes.DarkGray,
                    StrokeThickness = 0,
                    RadiusX = 10,
                    RadiusY = 10,
                };

                Rectangle filler = new Rectangle
                {
                    Width = 10,
                    Fill = Brushes.DarkGray,
                    StrokeThickness = 0,
                    Height = 40,
                };

                canvasMostUsedApps.Children.Add(otherRect);
                Canvas.SetLeft(otherRect, marginLeft);
                Canvas.SetTop(otherRect, 0);

                canvasMostUsedApps.Children.Add(filler);
                Canvas.SetLeft(filler, marginLeft);
                Canvas.SetTop(filler, 0);
            }

            void ListApps()
            {
                List<MostUsedAppsContext> listMostUsed = new List<MostUsedAppsContext>();
                for (int i = 0; i < (mostUsed.Count >= 3 ? 3 : mostUsed.Count); i++)
                {
                    listMostUsed.Add(new MostUsedAppsContext
                    (
                        mostUsed[i].AppName,
                        GetTimeString((mostUsed[i].UsedMinutes * 60) + mostUsed[i].UsedSeconds),
                        mostUsed[i].IconPath,
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorLists.colors[i]))
                    ));
                }

                lbMostUsed.ItemsSource = null;
                lbMostUsed.ItemsSource = listMostUsed;
            }

            void ListOther()
            {
                List<MostUsedAppsContext> listMostUsed = new List<MostUsedAppsContext>();
                for (int i = 3; i < mostUsed.Count; i++)
                {
                    listMostUsed.Add(new MostUsedAppsContext
                    (
                        mostUsed[i].AppName,
                        GetTimeString((mostUsed[i].UsedMinutes * 60) + mostUsed[i].UsedSeconds),
                        mostUsed[i].IconPath,
                        Brushes.DarkGray
                    ));
                }

                lbOther.ItemsSource = null;
                lbOther.ItemsSource = listMostUsed;
            }
        }

        void CreateUsageIntervalChart(GeneralData gData, List<AppUsage> appUsages)
        {
            cnvsIntervalls.Children.Clear();
            int spaces = 26;
            double cnvsWidth = cnvsIntervalls.ActualWidth;
            double cnvsHeight = cnvsIntervalls.ActualHeight;
            double hourDifference = cnvsWidth / spaces;

            for (int i = 1; i < spaces; i++)
            {
                Border b = new Border
                {
                    Height = 7,
                    Width = 2,
                    Background = Brushes.White,
                    Opacity = 0.85
                };
                TextBlock tb = new TextBlock
                {
                    Text = i == 25 ? (i - 1).ToString() + " h" : (i - 1).ToString(),
                    Style = (Style)Application.Current.Resources["ModeTextBlock"],
                    FontSize = 10,
                    Opacity = 0.7
                };

                cnvsIntervalls.Children.Add(b);
                cnvsIntervalls.Children.Add(tb);
                Canvas.SetLeft(b, i * hourDifference);
                Canvas.SetBottom(b, 17);
                Canvas.SetBottom(tb, 0);
                if (i > 10)
                {
                    Canvas.SetLeft(tb, i * hourDifference - 4.5);
                }
                else
                {
                    Canvas.SetLeft(tb, i * hourDifference - 1.5);
                }
            }

            List<MostUsedDiagramAppsContext> appList = new List<MostUsedDiagramAppsContext>();
            IntervallOfContext intervalls = GetIntervallsFromDateTimeList(gData.timeUsed, "system");

            foreach (var intervall in intervalls.Intervalls)
            {
                int hours = intervall.BisDate.Hour - intervall.VonDate.Hour;
                double bmv = intervall.VonDate.Minute / 6;
                double emv = intervall.BisDate.Minute / 6;
                double beginMinuteValue = bmv / 10;
                double endMinuteValue = emv / 10;

                Border b = new Border
                {
                    Height = 5,
                    CornerRadius = new CornerRadius(2),
                    Width = hourDifference * (hours + endMinuteValue - beginMinuteValue),
                    Background = Brushes.DarkGray,
                    ToolTip = $"{intervall.Name}: {intervall.VonDate.ToShortTimeString()} - {intervall.BisDate.ToShortTimeString()}"
                };

                cnvsIntervalls.Children.Add(b);
                Canvas.SetLeft(b, (intervall.VonDate.Hour + 1 + beginMinuteValue) * hourDifference);
                Canvas.SetBottom(b, 33);
            }
            appList.Add(new MostUsedDiagramAppsContext("System", Brushes.DarkGray, @"pack://application:,,,/ActivityPulse;component/Images/Resources/system.png"));

            List<AppUsage> mostUsed = appUsages.OrderByDescending(t => t.UsedMinutes).ThenByDescending(t => t.UsedSeconds).ToList();
            List<IntervallOfContext> appIntervalls = new List<IntervallOfContext>();

            for (int i = 0; i < 6; i++)
            {
                if (i < mostUsed.Count && mostUsed[i].UsedMinutes >= 3)
                {
                    appIntervalls.Add(new IntervallOfContext(mostUsed[i].AppName, GetIntervallsFromDateTimeList(mostUsed[i].timeUsed, mostUsed[i].AppName).Intervalls, mostUsed[i].IconPath));
                }
                else
                {
                    break;
                }
            }

            int height = 33;
            for (int i = 0; i < appIntervalls.Count; i++)
            {
                height += 13;
                foreach (var intervall in appIntervalls[i].Intervalls)
                {
                    int hours = intervall.BisDate.Hour - intervall.VonDate.Hour;
                    double bmv = intervall.VonDate.Minute / 6;
                    double emv = intervall.BisDate.Minute / 6;
                    double beginMinuteValue = bmv / 10;
                    double endMinuteValue = emv / 10;

                    Border b = new Border
                    {
                        Height = 5,
                        CornerRadius = new CornerRadius(2),
                        Width = hourDifference * (hours + endMinuteValue - beginMinuteValue) >= 0 ? hourDifference * (hours + endMinuteValue - beginMinuteValue) : 0,
                        Background = i < ColorLists.colors.Count ? new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorLists.colors[i])) : new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorLists.colors[0])),
                        ToolTip = $"{intervall.Name}: {intervall.VonDate.ToShortTimeString()} - {intervall.BisDate.ToShortTimeString()}"
                    };

                    cnvsIntervalls.Children.Add(b);
                    Canvas.SetLeft(b, (intervall.VonDate.Hour + 1 + beginMinuteValue) * hourDifference);
                    Canvas.SetBottom(b, height);
                }

                appList.Add(new MostUsedDiagramAppsContext(appIntervalls[i].Name, i < ColorLists.colors.Count ? new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorLists.colors[i])) : new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorLists.colors[0])), appIntervalls[i].IconPath));
            }
            lbApps.ItemsSource = null;
            lbApps.ItemsSource = appList;
        }

        IntervallOfContext GetIntervallsFromDateTimeList(List<DateTime> dateTimeList, string name)
        {
            IntervallOfContext intervalls = new IntervallOfContext();
            intervalls.Name = name;

            if (dateTimeList.Count >= 1)
            {
                DateTime oldDt = dateTimeList[0];
                DateTime begin = dateTimeList[0];

                foreach (DateTime dt in dateTimeList)
                {
                    if (oldDt.Minute != dt.Minute)
                    {
                        if ((dt - oldDt).TotalMinutes >= 12)
                        {
                            intervalls.Intervalls.Add(new IntervallContext(begin, oldDt, name));
                            begin = dt;
                        }
                    }
                    else if (dt.Hour != oldDt.Hour)
                    {
                        if ((dt - oldDt).TotalMinutes >= 12)
                        {
                            intervalls.Intervalls.Add(new IntervallContext(begin, oldDt, name));
                            begin = dt;
                        }
                    }

                    oldDt = dt;
                }

                intervalls.Intervalls.Add(new IntervallContext(begin, dateTimeList[dateTimeList.Count - 1], name));
            }

            return intervalls;
        }

        private void cnvsIntervalls_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CreateUsageIntervalChart(gData, appUsages);
        }

        private void canvasMostUsedApps_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CreateLineDiagramMostUsedApps(gData, appUsages);
        }

        private void dayBeforeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new TodayPage(day.AddDays(-1));
        }

        private void dayAfterBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Content = new TodayPage(day.AddDays(1));
        }
    }
}
