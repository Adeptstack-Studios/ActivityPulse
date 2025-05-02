using ActivityPulse.Utils;
using ActivityPulse.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ActivityPulse.UserControls
{
    /// <summary>
    /// Interaktionslogik für GetDateDialog.xaml
    /// </summary>
    /// 
    public delegate void DateDialogClick(DateTime date);

    public partial class GetDateDialog : UserControl
    {
        public event DateDialogClick OnDateDialogClick;
        private AverageType dateType;
        public AverageType DateType
        {
            get
            {
                return dateType;
            }
            set
            {
                dateType = value;

                if (dateType == AverageType.Week)
                {
                    TBDay.Visibility = Visibility.Visible;
                    TBMonth.Visibility = Visibility.Visible;
                    TBYear.Visibility = Visibility.Visible;
                }
                else if (dateType == AverageType.Month)
                {
                    TBDay.Visibility = Visibility.Collapsed;
                    TBMonth.Visibility = Visibility.Visible;
                    TBYear.Visibility = Visibility.Visible;
                }
                else if (dateType == AverageType.Year)
                {
                    TBDay.Visibility = Visibility.Collapsed;
                    TBMonth.Visibility = Visibility.Collapsed;
                    TBYear.Visibility = Visibility.Visible;
                }
            }
        }
        public string Title { get => tbTitle.Text; set => tbTitle.Text = value; }

        public GetDateDialog()
        {
            InitializeComponent();
        }

        private void closeDialog_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int day = TBDay.Text.Length >= 1 ? Convert.ToInt32(TBDay.Text) : 1;
                int month = TBMonth.Text.Length >= 1 ? Convert.ToInt32(TBMonth.Text) : 1;
                int year = Convert.ToInt32(TBYear.Text);
                if (year > 0 && month > 0 && day > 0 && month <= 12 && day <= DateTime.DaysInMonth(year, month))
                {
                    DateTime date = new DateTime(year, month, day);
                    TBDay.Text = string.Empty;
                    TBMonth.Text = string.Empty;
                    TBYear.Text = string.Empty;
                    OnDateDialogClick(date);
                    Close();
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

        public void Open()
        {
            //größer
            Visibility = Visibility.Visible;
            BeginStoryboard bg1 = this.FindResource("BGOPEN") as BeginStoryboard;
            bg1.Storyboard.Begin();

            BeginStoryboard db250 = this.FindResource("DWOPEN") as BeginStoryboard;
            db250.Storyboard.Begin();

            BeginStoryboard dh300 = this.FindResource("DHOPEN") as BeginStoryboard;
            dh300.Storyboard.Begin();
        }

        public void Close()
        {
            BeginStoryboard db0 = this.FindResource("DWCLOSE") as BeginStoryboard;
            db0.Storyboard.Begin();

            BeginStoryboard dh0 = this.FindResource("DHCLOSE") as BeginStoryboard;
            dh0.Storyboard.Begin();

            BeginStoryboard bg0 = this.FindResource("BGCLOSE") as BeginStoryboard;
            bg0.Storyboard.Completed += new EventHandler(delegate (object sender, EventArgs e)
            {
                Visibility = Visibility.Collapsed;
            });
            bg0.Storyboard.Begin();
        }
    }
}
