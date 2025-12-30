using System.Windows;
using System.Windows.Controls;

namespace ActivityPulse.UserControls
{
    /// <summary>
    /// Interaktionslogik für CountBoxControl.xaml
    /// </summary>
    public partial class CountBoxControl : UserControl
    {
        private int value = 0;
        public int Value { get => value; set { this.value = value; countTb.Text = value.ToString(); } }
        public int MaxValue { get; set; } = 0;
        private int minValue = 0;
        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; countTb.Text = Value.ToString(); }
        }

        public CountBoxControl()
        {
            InitializeComponent();
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
                    Value = MinValue;
                    countTb.Text = Value.ToString();
                }
            }
        }
    }
}
