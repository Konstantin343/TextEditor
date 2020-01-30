using System.Text;
using System.Windows;
using System.Windows.Controls;
using TextEditor.FileDialog;
using TextEditor.Highlight;
using TextEditor.Themes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private FileDialogManager FileDialogManager { get; }
        private ThemesManager ThemesManager { get; }

        public MainWindow()
        {
            InitializeComponent();
            TextEditBox.SetWordsToHighlight(BasicWordsToHighlight.JavaWords);
            FileDialogManager = new FileDialogManager(Encoding.UTF8, "All files|*.*|Text|*.txt|Java|*.java|C#|*.cs");
            ThemesManager = new ThemesManager(TextEditBox);
            Themes.ItemsSource = ThemesManager.GetThemesAsMenuItems(Theme_OnClick);
        }

        private void OpenFile_OnClick(object sender, RoutedEventArgs e)
        {
            var text = FileDialogManager.ReadTextFromFile();
            if (text == null) return;
            OpenedFileName.Text = FileDialogManager.CurrentOpenedFile;
            TextEditBox.SetTextLines(text);
        }

        private void SaveFile_OnClick(object sender, RoutedEventArgs e)
        {
            FileDialogManager.SaveTextInOpenedFile(TextEditBox.TextLines.RawLines);
            OpenedFileName.Text = FileDialogManager.CurrentOpenedFile;
        }

        private void SaveAsFile_OnClick(object sender, RoutedEventArgs e)
        {
            FileDialogManager.SaveTextInNewFile(TextEditBox.TextLines.RawLines);
            OpenedFileName.Text = FileDialogManager.CurrentOpenedFile;
        }

        private void NewFile_OnClick(object sender, RoutedEventArgs e)
        {
            FileDialogManager.NewFile(TextEditBox.TextLines.RawLines);
            TextEditBox.SetTextLines(new[] {""});
            OpenedFileName.Text = FileDialogManager.CurrentOpenedFile;
        }

        private void Theme_OnClick(object sender, RoutedEventArgs e)
        {
            ThemesManager.SelectTheme(((MenuItem) e.Source).Header.ToString());
            Themes.ItemsSource = ThemesManager.GetThemesAsMenuItems(Theme_OnClick);
        }
    }
}