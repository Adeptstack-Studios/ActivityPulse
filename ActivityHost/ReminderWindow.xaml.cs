using ActivityUtils.Models;
using System.ComponentModel;
using System.Media;
using System.Windows;

namespace ActivityHost
{
    /// <summary>
    /// Interaktionslogik für ReminderWindow.xaml
    /// </summary>
    public partial class ReminderWindow : Window
    {
        ReminderContext rc;

        bool isCompleted = false;
        public ReminderWindow(ReminderContext reminder)
        {
            InitializeComponent();
            rc = reminder;
            tbReminder.Text = reminder.Name;

            SoundPlayer p = new SoundPlayer();
            p.SoundLocation = Utils.GetSound(reminder.ReminderTypes, reminder.DayRepeatCount);
            p.Play();
        }

        private void dismissBTN_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void completeBTN_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.isCompleted = true;
            this.Close();
        }
    }
}
