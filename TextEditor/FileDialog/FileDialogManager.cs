using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using TextEditor.ViewModel;

namespace TextEditor.FileDialog
{
    public sealed class FileDialogManager : BaseNotifyPropertyChanged
    {
        public FileDialogManager(Encoding encoding, string filter)
        {
            Encoding = encoding;
            Filter = filter;
        }

        public string Filter { get; }

        public Encoding Encoding { get; }

        private string _currentOpenedFile;

        public string CurrentOpenedFile
        {
            get => _currentOpenedFile;
            private set
            {
                _currentOpenedFile = value;
                OnPropertyChanged(nameof(CurrentOpenedFile));
            }
        }

        public void SaveTextInNewFile(IEnumerable<string> text)
        {
            var saveFileDialog = new SaveFileDialog {Filter = Filter, Title = "Save File As"};
            if (saveFileDialog.ShowDialog() != true) return;
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            var fileName = saveFileDialog.FileName;
            File.WriteAllText(fileName, string.Join("\r\n", text), Encoding);
            if (string.IsNullOrEmpty(CurrentOpenedFile))
            {
                CurrentOpenedFile = fileName;
            }
        }

        public void SaveTextInOpenedFile(IEnumerable<string> text)
        {
            if (!string.IsNullOrEmpty(CurrentOpenedFile))
            {
                File.WriteAllText(CurrentOpenedFile, string.Join("\r\n", text), Encoding);
            }
            else
            {
                SaveTextInNewFile(text);
            }
        }

        public IList<string> ReadTextFromFile()
        {
            var openFileDialog = new OpenFileDialog {Filter = Filter, Title = "Open File"};
            if (openFileDialog.ShowDialog() != true || string.IsNullOrEmpty(openFileDialog.FileName))
                return null;
            CurrentOpenedFile = openFileDialog.FileName;
            var lines = File.ReadAllText(CurrentOpenedFile, Encoding);
            return Regex.Split(lines, "\r\n").ToList();
        }

        public void NewFile(IEnumerable<string> text)
        {
            if (!string.IsNullOrEmpty(CurrentOpenedFile))
                SaveTextInOpenedFile(text);
            CurrentOpenedFile = string.Empty;
        }
    }
}