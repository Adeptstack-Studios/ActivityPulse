using ActivityPulse.Windows;
using ActivityUtils.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ActivityPulse.UserControls
{
    /// <summary>
    /// Interaktionslogik für ManageCategoryControl.xaml
    /// </summary>

    public delegate void CategoryDialogClick(CategoryContext category);
    public partial class ManageCategoryControl : UserControl
    {
        public event CategoryDialogClick OnCategoryDialogClick;
        public string Title { get => tbTitle.Text; set => tbTitle.Text = value; }
        bool isEditMode;
        public bool IsEditMode { get => isEditMode; }
        CategoryContext editCategory;
        public CategoryContext EditCategory
        {
            set
            {
                editCategory = value;
                isEditMode = true;
                tbName.Text = value.Name;
            }
            get { return editCategory; }
        }

        public ManageCategoryControl()
        {
            InitializeComponent();
        }

        private void closeDialog_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbName.Text.Length > 0)
                {
                    if (!isEditMode)
                    {
                        OnCategoryDialogClick(new CategoryContext(tbName.Text));
                    }
                    else
                    {
                        editCategory.Name = tbName.Text;
                        OnCategoryDialogClick(editCategory);
                        isEditMode = false;
                    }
                    tbName.Text = "";
                    Close();
                }
                else
                {
                    TellBox tb = new TellBox("Please enter a name!", "Error");
                    tb.ShowDialog();
                }
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
