using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace TextEditComponent.TextEditComponent.TextHelpers
{
    public static class FormattedTextHelper
    {
        public static FormattedText GetFormattedText(
            string text,
            string fontStyle,
            double fontSize,
            Brush textBrush) =>
            new FormattedText(
                text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(fontStyle),
                fontSize,
                textBrush);

        public static double GetWidth(
            string text,
            string fontStyle,
            double fontSize) =>
            GetFormattedText(text, fontStyle, fontSize, Brushes.White).WidthIncludingTrailingWhitespace;
    }
}