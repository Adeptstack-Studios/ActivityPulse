using ActivityUtils.Utils;
using System.Windows;
using System.Windows.Controls;

namespace ActivityPulse.UserControls
{
    /// <summary>
    /// Interaktionslogik für TimePickerControl.xaml
    /// </summary>
    public partial class TimePickerControl : UserControl
    {
        public string Text
        {
            get { return tbTime.Text; }
            set { tbTime.Text = value; }
        }

        public TimePickerControl()
        {
            InitializeComponent();
            if (Utilities.Is24HForamt())
            {
                tbTime.Tag = "HH:mm";
                cbAMPM.Visibility = Visibility.Collapsed;
                cbcH.MinValue = 0;
                cbcH.MaxValue = 23;
            }
            else
            {
                tbTime.Tag = "hh:mm tt";
                cbAMPM.Visibility = Visibility.Visible;
                cbcH.MinValue = 1;
                cbcH.MaxValue = 12;
                cbAMPM.SelectedIndex = 0;
            }
        }

        private void tbTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Utilities.IsValidTime(Text))
            {
                Text = "";
            }
        }

        private void btnPopup_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Utilities.Is24HForamt())
            {
                int h = cbcH.Value;
                string m = cbcM.Value >= 10 ? cbcM.Value.ToString() : "0" + cbcM.Value;
                Text = $"{h}:{m}";
            }
            else
            {
                int h = cbcH.Value;
                string m = cbcM.Value >= 10 ? cbcM.Value.ToString() : "0" + cbcM.Value;
                string ampm = cbAMPM.Text;
                Text = $"{h}:{m} {ampm}";
            }
            popup.IsOpen = false;
        }
    }
}
