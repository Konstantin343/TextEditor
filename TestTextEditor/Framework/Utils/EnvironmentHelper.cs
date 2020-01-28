using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;

namespace TestTextEditor.Framework.Utils
{
    public static class EnvironmentHelper
    {
        public static readonly double PixelCoefficient =
            Screen.PrimaryScreen.Bounds.Width / SystemParameters.PrimaryScreenWidth;

        public static readonly string OutputDirectory =
            Regex.Replace(Assembly.GetExecutingAssembly().Location,
                $@"{Assembly.GetExecutingAssembly().GetName().Name}.dll$", "");

        public static string GetResourcePath(string resource) =>
            Path.Combine(OutputDirectory, "Resources", resource);

        public static string GetOutputPath(string resource) =>
            Path.Combine(OutputDirectory, resource);
    }
}