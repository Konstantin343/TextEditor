using System.Windows;
using System.Windows.Forms;

namespace TestTextEditor.Framework.Utils
{
    public static class EnvironmentHelper
    {
        public static readonly double PixelCoefficient =
            Screen.PrimaryScreen.Bounds.Width / SystemParameters.PrimaryScreenWidth;
    }
}