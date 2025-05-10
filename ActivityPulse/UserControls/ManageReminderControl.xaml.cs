using ActivityPulse.Windows;
using ActivityUtils.Enums;
using ActivityUtils.Models;
using ActivityUtils.Utils;
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
        List<CategoryContext> categories;
        public List<CategoryContext> Categories
        {
            set
            {
                categories = value;
                cbCategory.Items.Clear();
                cbCategory.Items.Add("None");
                foreach (var item in value)
                {
                    cbCategory.Items.Add(item.Name);
                }
                cbCategory.SelectedIndex = 0;
            }
        }
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
                if (tbName.Text.Length > 0 && dpTB.Text.Length > 0 && tpTB.Text.Length > 0)
                {
                    string name = tbName.Text;
                    DateTime date = dpTB.DisplayDate;
                    if (Utilities.TryParseTimeToDateTime(tpTB.Text, date, out DateTime result))
                    {
                        date = result;
                        TellBox tellBox = new TellBox(date.ToString(), "Error");
                        tellBox.ShowDialog();
                    }
                    EReminderTypes rType = (EReminderTypes)cbReminderType.SelectedIndex;
                    Guid categoryID = cbCategory.SelectedIndex > 0 ? categories[cbCategory.SelectedIndex - 1].Id : Guid.Empty;

                    bool important = (bool)chImportant.IsChecked;
                    bool force = (bool)chForce.IsChecked;
                    bool repeat = (bool)chRepeat.IsChecked;

                    if (tbIntervall.Text.Length > 0)
                    {
                        tbIntervall.Text = "1";
                    }
                    if (tbQuantity.Text.Length > 0)
                    {
                        tbQuantity.Text = "1";
                    }
                    if (dpUntil.Text.Length > 0)
                    {
                        dpUntil.Text = DateTime.Now.AddDays(100).ToShortDateString();
                    }

                    int rIntervall = int.Parse(tbIntervall.Text);
                    ERepeatTypes rpType = (ERepeatTypes)cbRepeat.SelectedIndex;
                    ERepeatDuration rDuration = (ERepeatDuration)cbDuration.SelectedIndex;
                    int quantity = int.Parse(tbQuantity.Text);
                    DateTime dtUntil = dpUntil.DisplayDate;

                    RepeatingContext repeatingContext = new RepeatingContext(rpType, rIntervall, rDuration, quantity, dtUntil);
                    ReminderContext reminder = new ReminderContext(name, date, repeatingContext, rType, categoryID, repeat, force, important);
                    reminder.NextRemind = date;
                }
                else
                {
                    TellBox tellBox = new TellBox("You may forgot some inputs", "Error");
                    tellBox.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                TellBox tellBox = new TellBox(ex.ToString(), "Error");
                tellBox.ShowDialog();
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

        private void chRepeat_Click(object sender, RoutedEventArgs e)
        {
            repeatSP.Visibility = chRepeat.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        private void cbReminderType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chForce.Visibility = cbReminderType.SelectedIndex == 1 || cbReminderType.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void cbDuration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbQuantity.Visibility = cbDuration.SelectedIndex == 1 ? Visibility.Visible : Visibility.Collapsed;
            dpUntil.Visibility = cbDuration.SelectedIndex == 2 ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
