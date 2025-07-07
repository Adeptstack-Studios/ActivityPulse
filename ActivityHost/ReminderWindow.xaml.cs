using ActivityUtils.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ActivityHost
{
    /// <summary>
    /// Interaktionslogik für ReminderWindow.xaml
    /// </summary>
    public partial class ReminderWindow : Window
    {
        public ReminderWindow(ReminderContext reminder)
        {
            InitializeComponent();
            tbReminder.Text = reminder.Name;
            me.LoadedBehavior = MediaState.Manual;
            me.Source = "pack://application:,,,/ActivityHost;component/sounds/1-3.mp3";
            me.Volume = 1;
            me.Stop();
            me.Play();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            DialogResult = false;
        }

        private void dismissBTN_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void completeBTN_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
