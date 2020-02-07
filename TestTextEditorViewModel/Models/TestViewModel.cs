using System.Collections.Generic;
using System.Linq;
using TestTools.Logger;
using TextEditor.ViewModel;

namespace TestTextEditorViewModel.Models
{
    public class TestViewModel
    {
        public TextEditorViewModel TextEditorViewModel { get; }

        public TestViewModel() =>
            TextEditorViewModel = new TextEditorViewModel();

        public void SetText(IList<string> rawTextLines)
        {
            TestLogger.Instance.Info("Set text: \r\n" + string.Join("\r\n", rawTextLines));
            TextEditorViewModel.RawTextLines = rawTextLines;
        }

        public void NewFile()
        {
            TestLogger.Instance.Info("Select 'New file' command");
            TextEditorViewModel.NewFile();
        }

        public void SaveFile()
        {
            TestLogger.Instance.Info("Select 'Save file' command");
            TextEditorViewModel.SaveFile();
        }

        public void SaveAsFile(string fileName)
        {
            TestLogger.Instance.Info($"Select 'Save as file' command with '{fileName}'");
            TextEditorViewModel.SaveAsFile(fileName);
        }

        public void OpenFile(string fileName)
        {
            TestLogger.Instance.Info($"Select 'Open file' command with '{fileName}'");
            TextEditorViewModel.OpenFile(fileName);
        }

        public void SelectTheme(string themeName)
        {
            TestLogger.Instance.Info($"Select theme '{themeName}'");
            TextEditorViewModel.SelectThemeCommand.Execute(themeName);
        }

        public string Text => string.Join("\r\n", TextEditorViewModel.RawTextLines);
        
        public IList<string> Themes
        {
            get
            {
                var themes = TextEditorViewModel.ThemesService.Themes.Select(t => t.Name).ToList();
                TestLogger.Instance.Info("Got themes '" + string.Join(", ", themes) + "'");
                return themes;
            }
        }

        public string CurrentTheme
        {
            get
            {
                var currentTheme = TextEditorViewModel.ThemesService.CurrentTheme.Name;
                TestLogger.Instance.Info($"Got current theme: {currentTheme}");
                return currentTheme;
            }
        }
        
        public string CurrentOpenedFile
        {
            get
            {
                var currentOpenedFile = TextEditorViewModel.FileService.CurrentOpenedFile;
                TestLogger.Instance.Info($"Got current opened file: {currentOpenedFile}");
                return currentOpenedFile;
            }
        }

        public ISet<string> WordsToHighlight
        {
            get
            {
                var words = TextEditorViewModel.HighlightService.WordsToHighlight;
                TestLogger.Instance.Info("Got words to highlight '" + string.Join(", ", words) + "'");
                return words;
            }
        }
    }
}