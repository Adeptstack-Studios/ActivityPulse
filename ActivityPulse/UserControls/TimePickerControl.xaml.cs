using System.Windows;
using System.Windows.Controls;

namespace ActivityPulse.UserControls
{
    /// <summary>
    /// Interaktionslogik für TimePickerControl.xaml
    /// </summary>
    public partial class TimePickerControl : UserControl
    {
        public TimePickerControl()
        {
            InitializeComponent();
        }

        private void tbTime_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnPopup_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }
    }
}
