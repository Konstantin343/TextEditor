using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using TextEditComponent;
using TextEditComponent.TextEditComponent.Helpers;

namespace TestTextEditor.Helpers
{
    public static class PointHelper
    {
        public static readonly double PixelCoefficient =
            Screen.PrimaryScreen.Bounds.Width / SystemParameters.PrimaryScreenWidth;
        
        public static Point GetPointToClickOn(int str, int chr, IList<string> text)
        {
            var y = str * (Settings.LineInterval + Settings.FontSize);
            var x = FormattedTextHelper.GetWidth(
                        text[str].Substring(0, chr),
                        Settings.FontStyle, Settings.FontSize);
            return new Point(x * PixelCoefficient, y * PixelCoefficient);
        }
    }
}