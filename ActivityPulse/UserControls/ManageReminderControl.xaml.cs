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
                dpTB.Text = value.ReminderDateTime.ToShortDateString();
                tpTB.Text = value.ReminderDateTime.ToShortTimeString();
                cbReminderType.SelectedIndex = (int)value.ReminderTypes;
                cbCategory.SelectedIndex = categories.FindIndex(p => p.Id == editReminder.CategoryId) + 1;
                chImportant.IsChecked = value.IsImportant;
                chForce.IsChecked = value.IsForced;
                chRepeat.IsChecked = value.DoRepeat;
                tbIntervall.Text = value.Repeating.RepeatInterval.ToString();
                cbRepeat.SelectedIndex = (int)value.Repeating.RepeatType;
                cbDuration.SelectedIndex = (int)value.Repeating.RepeatDuration;
                tbQuantity.Text = value.Repeating.RepeatCount.ToString();
                dpUntil.Text = value.Repeating.RepeatUntil.ToShortDateString();
                repeatSP.Visibility = value.DoRepeat ? Visibility.Visible : Visibility.Collapsed;
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
                    DateTime date = (DateTime)dpTB.SelectedDate;
                    if (TimeUtils.TryParseTimeToDateTime(tpTB.Text, date, out DateTime result))
                    {
                        date = result;
                    }
                    EReminderTypes rType = (EReminderTypes)cbReminderType.SelectedIndex;
                    int categoryID = cbCategory.SelectedIndex > 0 ? categories[cbCategory.SelectedIndex - 1].Id : 0;

                    bool important = (bool)chImportant.IsChecked;
                    bool force = (bool)chForce.IsChecked;
                    bool repeat = (bool)chRepeat.IsChecked;

                    if (tbIntervall.Text.Length == 0)
                    {
                        tbIntervall.Text = "1";
                    }
                    if (tbQuantity.Text.Length == 0)
                    {
                        tbQuantity.Text = "1";
                    }
                    if (dpUntil.Text.Length == 0)
                    {
                        dpUntil.Text = DateTime.Now.AddDays(100).ToShortDateString();
                    }

                    int rIntervall = int.Parse(tbIntervall.Text);
                    ERepeatTypes rpType = (ERepeatTypes)cbRepeat.SelectedIndex;
                    ERepeatDuration rDuration = (ERepeatDuration)cbDuration.SelectedIndex;
                    int quantity = int.Parse(tbQuantity.Text);
                    DateTime dtUntil = (DateTime)dpUntil.SelectedDate;

                    if (!isEditMode)
                    {
                        RepeatingContext repeatingContext = new RepeatingContext(rpType, rIntervall, rDuration, quantity, dtUntil);
                        ReminderContext reminder = new ReminderContext(name, date, repeatingContext, rType, categoryID, repeat, force, important);
                        reminder.NextRemind = date;
                        reminder.Source = "ActivityPulse";
                        OnReminderDialogClick(reminder);
                    }
                    else
                    {
                        editReminder.Name = name;
                        editReminder.ReminderDateTime = date;
                        editReminder.ReminderTypes = rType;
                        editReminder.CategoryId = categoryID;
                        editReminder.IsImportant = important;
                        editReminder.IsForced = force;
                        editReminder.DoRepeat = repeat;
                        editReminder.Repeating.RepeatInterval = rIntervall;
                        editReminder.Repeating.RepeatType = rpType;
                        editReminder.Repeating.RepeatDuration = rDuration;
                        editReminder.Repeating.RepeatCount = quantity;
                        editReminder.Repeating.RepeatUntil = dtUntil;
                        OnReminderDialogClick(editReminder);
                    }
                    Close();
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
                Clear();
            });
            bg0.Storyboard.Begin();
        }

        private void Clear()
        {
            tbName.Text = "";
            dpTB.Text = "";
            tpTB.Text = "";
            chImportant.IsChecked = false;
            chForce.IsChecked = false;
            chRepeat.IsChecked = false;
            tbIntervall.Text = "1";
            tbQuantity.Text = "1";
            dpUntil.Text = "";
            isEditMode = false;
            cbReminderType.SelectedIndex = 0;
            cbCategory.SelectedIndex = 0;
            cbRepeat.SelectedIndex = 1;
            cbDuration.SelectedIndex = 0;
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
