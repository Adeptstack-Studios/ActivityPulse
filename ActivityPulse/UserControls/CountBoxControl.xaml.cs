using System.Windows;
using System.Windows.Controls;

namespace ActivityPulse.UserControls
{
    /// <summary>
    /// Interaktionslogik für CountBoxControl.xaml
    /// </summary>
    public partial class CountBoxControl : UserControl
    {
        public int Value { get; set; } = 0;
        public int MaxValue { get; set; } = 0;
        public int MinValue { get; set; } = 0;

        public CountBoxControl()
        {
            InitializeComponent();
            countTb.Text = Value.ToString();
        }

        private void downBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Value > MinValue)
            {
                Value--;
            }
            else
            {
                Value = MaxValue;
            }

            countTb.Text = Value.ToString();
        }

        private void upBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Value < MaxValue)
            {
                Value++;
            }
            else
            {
                Value = MinValue;
            }
            countTb.Text = Value.ToString();
        }

        private void countTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (countTb.Text.Length > 0)
            {
                Value = int.Parse(countTb.Text);
                if (Value > MaxValue || Value < MinValue)
                {
                    Value = 0;
                    countTb.Text = Value.ToString();
                }
            }
        }
    }
}
