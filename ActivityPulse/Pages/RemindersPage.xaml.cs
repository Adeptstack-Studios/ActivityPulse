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
            categoryDialog.Title = "Edit category";
            categoryDialog.EditCategory = (CategoryContext)lbCategories.SelectedItem;
            categoryDialog.Open();
        }

        private void miDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
