using ActivityPulse.Utils;
using ActivityPulse.Windows;
using System.Windows;
using System.Windows.Controls;

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
            this.Visibility = Visibility.Collapsed;
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
                    this.Visibility = Visibility.Collapsed;
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
    }
}
