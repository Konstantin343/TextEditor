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
using Utils;

namespace TextEditor.ViewModel
{
    public class TextEditorViewModel : BaseNotifyPropertyChanged
    {
        private IFileService _fileService;

        private IDialogService _dialogService;

        private IThemesService _themesService;

        private IHighlightService _highlightService;

        private IList<string> _rawTextLines;

        public TextEditorViewModel()
        {
            DialogService = new DialogService("All files|*.*|Text|*.txt|Java|*.java|C#|*.cs");
            FileService = new FileService(Encoding.UTF8);
            ThemesService = new ThemesService(BasicThemes.AllBasicThemes);
            HighlightService = new HighlightService(BasicWordsToHighlight.CsWords);
            RawTextLines = new List<string>(new[] {""});
        }

        public IFileService FileService
        {
            get => _fileService;
            set
            {
                _fileService = value;
                OnPropertyChanged(nameof(FileService));
            }
        }

        public IDialogService DialogService
        {
            get => _dialogService;
            set
            {
                _dialogService = value;
                OnPropertyChanged(nameof(DialogService));
            }
        }

        public IThemesService ThemesService
        {
            get => _themesService;
            set
            {
                _themesService = value;
                OnPropertyChanged(nameof(ThemesService));
            }
        }

        public IHighlightService HighlightService
        {
            get => _highlightService;
            set
            {
                _highlightService = value;
                OnPropertyChanged(nameof(HighlightService));
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
                ThemesService.Themes.Select(t => new MenuItem
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
                FileService.SaveAndCreateNewFile(textLines);
                RawTextLines = new List<string>(new[] {""});
                HighlightService.SetWordsToHighlight(FileService.CurrentOpenedFile);
            }));

        private ICommand _openFileCommand;

        public ICommand OpenFileCommand =>
            _openFileCommand ??
            (_openFileCommand = new RelayCommand(obj => OpenFile(DialogService.OpenFileDialog())));

        public void OpenFile(string fileName)
        {
            var text = FileService.OpenNewFile(fileName);
            if (text == null) return;
            RawTextLines = text;
            HighlightService.SetWordsToHighlight(FileService.CurrentOpenedFile);
        }

        private ICommand _saveFileCommand;

        public ICommand SaveFileCommand =>
            _saveFileCommand ??
            (_saveFileCommand = new RelayCommand(obj =>
            {
                var textLines = (obj as TextLines)?.RawLines;
                RawTextLines = textLines;
                if (!string.IsNullOrEmpty(FileService.CurrentOpenedFile))
                    SaveFile();
                else
                    SaveAsFile(DialogService.SaveFileDialog());

                HighlightService.SetWordsToHighlight(FileService.CurrentOpenedFile);
            }));

        public void SaveFile() => FileService.SaveTextInCurrentFile(RawTextLines);

        private ICommand _saveAsFileCommand;

        public ICommand SaveAsFileCommand =>
            _saveAsFileCommand ??
            (_saveAsFileCommand = new RelayCommand(obj =>
            {
                var textLines = (obj as TextLines)?.RawLines;
                RawTextLines = textLines;
                SaveAsFile(DialogService.SaveFileDialog());
                HighlightService.SetWordsToHighlight(FileService.CurrentOpenedFile);
            }));

        public void SaveAsFile(string fileName) =>
            FileService.SaveTextInFile(fileName, RawTextLines);

        private ICommand _selectThemeCommand;

        public ICommand SelectThemeCommand =>
            _selectThemeCommand ??
            (_selectThemeCommand = new RelayCommand(obj => { ThemesService.SelectTheme((string) obj); }));
    }
}