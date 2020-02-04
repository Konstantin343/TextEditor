using System.Windows.Media;

namespace TextEditComponent
{
    public static class Settings
    {
        public const int FontSize = 14;
        public const int PaddingLeft = 2;
        public const int LineInterval = 2;
        public const string FontStyle = "Verdana";
        public const int HorizontalDelta = 10;
        public const int VerticalDelta = 5;
        public const double CaretHeightParameter = 1.2;
        public const int BorderWidth = 1;
        public static readonly Brush BorderBrush = Brushes.LightSkyBlue;
        public static readonly Brush Background = Brushes.White;
        public static readonly Brush HighlightBrush = Brushes.DarkBlue;
        public static readonly Brush TextBrush = Brushes.Black;
        public const bool Focusable = true;
    }
}