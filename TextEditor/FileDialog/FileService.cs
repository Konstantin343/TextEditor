using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextEditor.ViewModel;

namespace TextEditor.FileDialog
{
    public class FileService : BaseNotifyPropertyChanged, IFileService
    {
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

        public FileService(Encoding encoding)
        {
            Encoding = encoding;
        }

        public Encoding Encoding { get; }

        public List<string> OpenNewFile(string fileName)
        {
            CurrentOpenedFile = fileName;
            return Regex.Split(File.ReadAllText(CurrentOpenedFile, Encoding), "\r\n").ToList();
        }

        public void SaveTextInFile(string fileName, IEnumerable<string> text)
        {
            if (string.IsNullOrEmpty(CurrentOpenedFile))
                CurrentOpenedFile = fileName;

            File.WriteAllText(fileName, string.Join("\r\n", text));
        }

        public void SaveTextInCurrentFile(IEnumerable<string> text)
        {
            if (string.IsNullOrEmpty(CurrentOpenedFile))
                return;
            
            File.WriteAllText(CurrentOpenedFile, string.Join("\r\n", text));
        }

        public void SaveAndCreateNewFile(IEnumerable<string> text)
        {
            if (!string.IsNullOrEmpty(CurrentOpenedFile))
                SaveTextInFile(CurrentOpenedFile, text);
            CurrentOpenedFile = string.Empty;
        }
    }
}