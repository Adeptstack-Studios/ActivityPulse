using System.Windows.Media;

namespace ActivityPulse.Utils
{
    public class ColorLists
    {
        public static List<string> colors = new List<string>
        {
            "#6a1b9a",
            "#7b1fa2",
            "#8e24aa",
            "#9c27b0",
            "#ab47bc"
        };

        public static List<Brush> monthBrushes = new List<Brush>
        {
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#AEDFF7")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8A0C9")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7FC97F")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD966")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFB347")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6F61")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD700")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D4A373")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7518")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A0522D")),
            new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B22222")),
        };
    }
}
