using ActivityPulse.Models;
using ActivityUtilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ActivityPulse.Pages
{
    /// <summary>
    /// Interaktionslogik für AveragePage.xaml
    /// </summary>
    public partial class AveragePage : Page
    {
        List<GeneralData> gDatas = new List<GeneralData>();
        List<List<AppUsage>> appDayUsages = new List<List<AppUsage>>();
        List<DateTime> days;
        AverageType avgType;

        List<string> colors = new List<string>
        {
            "#6a1b9a",
            "#7b1fa2",
            "#8e24aa",
            "#9c27b0",
            "#ab47bc"
        };

        public AveragePage(List<DateTime> days, AverageType type)
        {
            InitializeComponent();
            this.days = days;
            this.avgType = type;
            MainWindow.UpdateTitle($"ActivityPulse - {days[0].ToShortDateString()} - {days[days.Count - 1].ToShortDateString()}");

            tbkTitle.Text = days[0].ToShortDateString() + " - " + days[days.Count - 1].ToShortDateString();
            GetData();
            DisplayScreenTime();
        }

        void DisplayScreenTime()
        {
            long totalSeconds = 0;
            foreach (GeneralData data in gDatas)
            {
                totalSeconds += data.gesSecondsUsed;
            }
            MessageBox.Show(gDatas.Count.ToString());
            tbkAVGScreenTime.Text = TodayPage.GetTimeString(totalSeconds / gDatas.Count);
            tbkScreenTime.Text = TodayPage.GetTimeString(totalSeconds);
        }

        void GetData()
        {
            foreach (DateTime day in days)
            {
                gDatas.Add(TodayPage.GetGeneralData(day));
                appDayUsages.Add(TodayPage.GetAppUsages(day));
            }
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

                MessageBox.Show(gesAppTime.ToString());
                MessageBox.Show(gesUsedTime.ToString());
                MessageBox.Show(percentige.ToString());

                Rectangle rect = new Rectangle
                {
                    Height = 40,
                    Width = canavsWidth * percentige,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i])),
                    StrokeThickness = 0,
                };

                if (i == 0)
                {
                    rect.RadiusX = 10;
                    rect.RadiusY = 10;

                    Rectangle filler = new Rectangle
                    {
                        Width = 10,
                        Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i])),
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
                        TodayPage.GetTimeString((mostUsed[i].UsedMinutes * 60) + mostUsed[i].UsedSeconds),
                        mostUsed[i].IconPath,
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString(colors[i]))
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
                        TodayPage.GetTimeString((mostUsed[i].UsedMinutes * 60) + mostUsed[i].UsedSeconds),
                        mostUsed[i].IconPath,
                        Brushes.DarkGray
                    ));
                }

                lbOther.ItemsSource = null;
                lbOther.ItemsSource = listMostUsed;
            }
        }

        private void GenerateIntervallDiagramm(List<GeneralData> genDatas)
        {
            cnvsIntervalls.Children.Clear();
            int spaces = genDatas.Count + 1;
            double cnvsWidth = cnvsIntervalls.ActualWidth;
            double cnvsHeight = cnvsIntervalls.ActualHeight;
            double intervallDifference = cnvsWidth / spaces;

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
                    Text = genDatas[i - 1].timeUsed[0].ToShortDateString(),
                    Style = (Style)Application.Current.Resources["ModeTextBlock"],
                    FontSize = 10,
                    Opacity = 0.7,
                    RenderTransform = new RotateTransform(270)
                };

                cnvsIntervalls.Children.Add(b);
                cnvsIntervalls.Children.Add(tb);
                Canvas.SetLeft(b, i * intervallDifference);
                Canvas.SetBottom(b, -20);
                Canvas.SetBottom(tb, -95);
                if (i > 10)
                {
                    Canvas.SetLeft(tb, i * intervallDifference - 6);
                }
                else
                {
                    Canvas.SetLeft(tb, i * intervallDifference - 6);
                }
            }
        }

        private void canvasMostUsedApps_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GeneralData g = new GeneralData();
            List<AppUsage> appUsage = new List<AppUsage>();
            long totalSeconds = 0;

            foreach (GeneralData data in gDatas)
            {
                totalSeconds += data.gesSecondsUsed;
                foreach (var item in data.timeUsed)
                {
                    g.timeUsed.Add(item);
                }
            }
            g.gesSecondsUsed = totalSeconds;

            foreach (var item in appDayUsages)
            {
                foreach (var day in item)
                {
                    //addieren der Zeiten bereits hinzugefügter Apps
                    bool success = true;
                    int index = 0;
                    for (int i = 0; i < appUsage.Count; i++)
                    {
                        if (appUsage[i].AppName == day.AppName)
                        {
                            success = false;
                            index = i;
                        }
                    }

                    if (success)
                    {
                        appUsage.Add(day);
                    }
                    else
                    {
                        int seconds = appUsage[index].UsedSeconds + day.UsedSeconds;
                        appUsage[index].UsedMinutes += day.UsedMinutes + (seconds / 60);
                        appUsage[index].UsedSeconds = seconds % 60;
                    }
                }
            }

            CreateLineDiagramMostUsedApps(g, appUsage);
        }

        private void cnvsIntervalls_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GenerateIntervallDiagramm(gDatas);
        }
    }
}
