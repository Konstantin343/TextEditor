using System.Reflection;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using TestTextEditor.Framework.Utils.Logger;
using TextEditor;

namespace TestTextEditor.Framework
{
    public static class TextEditorAppLoader
    {
        private static readonly string AppExecutablePath = Assembly.GetAssembly(typeof(MainWindow)).Location;
        private const string MainWindowHandle = "Text editor";

        private static Application _application;
        private static Window _window;

        public static void StartApp()
        {
            TestLogger.Instance.Info($"Starting App 'TextEditor' from {AppExecutablePath}");
            _application = Application.Launch(AppExecutablePath);
            _window = _application.GetWindow(MainWindowHandle);
        }

        public static Window Window => _window;

        public static void CloseApp()
        {
            TestLogger.Instance.Info("Closing App 'TextEditor'");
            _application.Close();
            _application = null;
            _window = null;
        }
    }
}