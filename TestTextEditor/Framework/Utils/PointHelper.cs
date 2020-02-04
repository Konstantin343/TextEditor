using System.Collections.Generic;
using System.Windows;
using TextEditComponent;
using TextEditComponent.TextEditComponent.TextHelpers;

namespace TestTextEditor.Framework.Utils
{
    public static class PointHelper
    {
        public static Point GetPointToClickOn(int str, int chr, IList<string> text)
        {
            var y = str * (Settings.LineInterval + Settings.FontSize);
            var x = FormattedTextHelper.GetWidth(
                        text[str].Substring(0, chr),
                        Settings.FontStyle, Settings.FontSize);
            return new Point(x * EnvironmentHelper.PixelCoefficient, y * EnvironmentHelper.PixelCoefficient);
        }
    }
}