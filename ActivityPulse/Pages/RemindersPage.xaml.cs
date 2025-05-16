using ActivityUtils.Data;
using ActivityUtils.Models;
using System.Windows.Controls;

namespace ActivityPulse.Pages
{
    /// <summary>
    /// Interaktionslogik für RemindersPage.xaml
    /// </summary>
    public partial class RemindersPage : Page
    {
        List<ReminderContext> reminders;
        List<CategoryContext> categories;

        public RemindersPage()
        {
            InitializeComponent();
            MainWindow.UpdateTitle("ActivityPulse - Reminders");
            categories = DataReminders.LoadCategories();
            reminders = DataReminders.LoadReminders();
            lbCategories.ItemsSource = null;
            lbReminders.ItemsSource = null;
            lbCategories.ItemsSource = categories;
            lbReminders.ItemsSource = reminders;
        }

        private void addCategoryBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            categoryDialog.Title = "Add category";
            categoryDialog.Open();
        }

        private void allCategoriesBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            lbCategories.SelectedIndex = -1;
        }

        private void lbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCategories.SelectedIndex >= 0)
            {

            }
        }

        private void categoryDialog_OnCategoryDialogClick(CategoryContext category)
        {
            if (!categoryDialog.IsEditMode)
            {
                categories.Add(category);
            }
            else
            {
                if (lbCategories.SelectedIndex >= 0)
                {
                    if (categories[lbCategories.SelectedIndex].Id == category.Id)
                    {
                        categories[lbCategories.SelectedIndex] = category;
                    }
                }
            }
            lbCategories.ItemsSource = null;
            lbCategories.ItemsSource = categories;
            DataReminders.SaveCategories(categories);
        }

        private void miEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lbCategories.SelectedIndex >= 0)
            {
                categoryDialog.Title = "Edit category";
                categoryDialog.EditCategory = categories[lbCategories.SelectedIndex];
                categoryDialog.Open();
            }
        }

        private void miDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lbCategories.SelectedIndex >= 0)
            {
                int id = categories[lbCategories.SelectedIndex].Id;
                foreach (var item in reminders)
                {
                    if (item.CategoryId == id)
                    {
                        item.CategoryId = 0;
                    }
                }
                categories.RemoveAt(lbCategories.SelectedIndex);
                lbCategories.ItemsSource = null;
                lbCategories.ItemsSource = categories;
                DataReminders.SaveCategories(categories);
            }
        }

        private void reminderDialog_OnReminderDialogClick(ReminderContext reminder)
        {
            if (!reminderDialog.IsEditMode)
            {
                reminders.Insert(0, reminder);
            }
            else
            {
                if (i >= 0)
                {
                    reminders[i] = reminder;
                }
            }
            OrderList();
            lbReminders.ItemsSource = null;
            lbReminders.ItemsSource = reminders;
            DataReminders.SaveReminder(reminders);
        }

        private void addReminderBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            reminderDialog.Title = "Add reminder";
            reminderDialog.Categories = categories;
            reminderDialog.Open();
        }

        private void tbAddReminder_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                reminderDialog.Title = "Add reminder";
                reminderDialog.tbName.Text = tbAddReminder.Text;
                reminderDialog.Categories = categories;
                reminderDialog.Open();
                tbAddReminder.Text = "";
            }
        }

        private void CHB_Done_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lbReminders.SelectedIndex >= 0)
            {
                CheckBox ch = sender as CheckBox;
                if (ch.IsChecked == true)
                    reminders[lbReminders.SelectedIndex].IsCompleted = true;
                else
                    reminders[lbReminders.SelectedIndex].IsCompleted = false;
            }
        }

        int i = -1;
        private void Click_Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lbReminders.SelectedIndex >= 0)
            {
                i = lbReminders.SelectedIndex;
                reminderDialog.Title = "Edit reminder";
                reminderDialog.Categories = categories;
                reminderDialog.EditReminder = reminders[i];
                reminderDialog.Open();
            }
        }

        void OrderList()
        {
            reminders = reminders.OrderBy(o => o.NextRemind).ToList();
        }
    }
}
