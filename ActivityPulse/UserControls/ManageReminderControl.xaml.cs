using ActivityPulse.Windows;
using ActivityUtils.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ActivityPulse.UserControls
{
    /// <summary>
    /// Interaktionslogik für ManageReminderControl.xaml
    /// </summary>

    public delegate void ReminderDialogClick(ReminderContext reminder);
    public partial class ManageReminderControl : UserControl
    {
        public event ReminderDialogClick OnReminderDialogClick;
        public string Title { get => tbTitle.Text; set => tbTitle.Text = value; }
        bool isEditMode;
        public bool IsEditMode { get => isEditMode; }
        ReminderContext editReminder;
        public ReminderContext EditReminder
        {
            set
            {
                editReminder = value;
                isEditMode = true;
                tbName.Text = value.Name;
            }
            get { return editReminder; }
        }

        public ManageReminderControl()
        {
            InitializeComponent();
            cbReminderType.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            cbRepeat.SelectedIndex = 1;
            cbDuration.SelectedIndex = 0;
        }

        private void closeDialog_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

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
