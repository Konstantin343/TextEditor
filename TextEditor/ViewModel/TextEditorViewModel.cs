using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using TextEditComponent.TextEditComponent.Text;
using TextEditor.FileDialog;
using TextEditor.Highlight;
using TextEditor.Themes;

namespace TextEditor.ViewModel
{
    public class TextEditorViewModel : BaseNotifyPropertyChanged
    {
        private FileDialogManager _fileDialogManager;

        private ThemesManager _themesManager;

        private ISet<string> _wordsToHighlight;

        private IList<string> _rawTextLines;

        public TextEditorViewModel()
        {
            FileDialogManager = new FileDialogManager(
                Encoding.UTF8,
                "All files|*.*|Text|*.txt|Java|*.java|C#|*.cs");
            ThemesManager = new ThemesManager(BasicThemes.AllBasicThemes);
            WordsToHighlight = BasicWordsToHighlight.CsWords;
            _rawTextLines = new List<string>(new[] {""});
        }

        public FileDialogManager FileDialogManager
        {
            get => _fileDialogManager;
            set
            {
                _fileDialogManager = value;
                OnPropertyChanged(nameof(FileDialogManager));
            }
        }

        public ThemesManager ThemesManager
        {
            get => _themesManager;
            set
            {
                _themesManager = value;
                OnPropertyChanged(nameof(ThemesManager));
            }
        }

        public ISet<string> WordsToHighlight
        {
            get => _wordsToHighlight;
            set
            {
                _wordsToHighlight = value;
                OnPropertyChanged(nameof(WordsToHighlight));
            }
        }

        public IList<string> RawTextLines
        {
            get => _rawTextLines;
            set
            {
                _rawTextLines = value;
                OnPropertyChanged(nameof(RawTextLines));
            }
        }

        public ObservableCollection<MenuItem> MenuItemThemes =>
            new ObservableCollection<MenuItem>(
                ThemesManager.Themes.Select(t => new MenuItem
                {
                    Header = t.Name,
                    Command = SelectThemeCommand,
                    CommandParameter = t.Name,
                    Uid = t.Name
                }));

        private ICommand _newFileCommand;

        public ICommand NewFileCommand =>
            _newFileCommand ??
            (_newFileCommand = new RelayCommand(obj =>
            {
                var textLines = (obj as TextLines)?.RawLines;
                FileDialogManager.NewFile(textLines);
                RawTextLines = new List<string>(new[] {""});
                UpdateWordsToHighlight();
            }));

        private ICommand _openFileCommand;

        public ICommand OpenFileCommand =>
            _openFileCommand ??
            (_openFileCommand = new RelayCommand(obj =>
            {
                var text = FileDialogManager.ReadTextFromFile();
                if (text == null) return;
                RawTextLines = text;
                UpdateWordsToHighlight();
            }));

        private ICommand _saveFileCommand;

        public ICommand SaveFileCommand =>
            _saveFileCommand ??
            (_saveFileCommand = new RelayCommand(obj =>
            {
                var textLines = (obj as TextLines)?.RawLines;
                FileDialogManager.SaveTextInOpenedFile(textLines);
                UpdateWordsToHighlight();
            }));

        private ICommand _saveAsFileCommand;

        public ICommand SaveAsFileCommand =>
            _saveAsFileCommand ??
            (_saveAsFileCommand = new RelayCommand(obj =>
            {
                var textLines = (obj as TextLines)?.RawLines;
                FileDialogManager.SaveTextInNewFile(textLines);
                UpdateWordsToHighlight();
            }));

        private ICommand _selectThemeCommand;

        public ICommand SelectThemeCommand =>
            _selectThemeCommand ??
            (_selectThemeCommand = new RelayCommand(obj => { ThemesManager.SelectTheme((string) obj); }));

        private void UpdateWordsToHighlight() =>
            WordsToHighlight = LanguageMapper.GetLanguageByName(FileDialogManager.CurrentOpenedFile).Map();
    }
}