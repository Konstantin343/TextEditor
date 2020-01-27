using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace TextEditor.FileDialog
{
    internal class FileDialogManager
    {
        public FileDialogManager(Encoding encoding, string filter)
        {
            Encoding = encoding;
            Filter = filter;
        }

        public string Filter { get; }

        public string CurrentOpenedFile { get; private set; }

        public Encoding Encoding { get; }

        public void SaveTextInNewFile(IEnumerable<string> text)
        {
            var saveFileDialog = new SaveFileDialog {Filter = Filter};
            if (saveFileDialog.ShowDialog() != true) return;
            if (string.IsNullOrEmpty(saveFileDialog.FileName)) return;
            var fileName = saveFileDialog.FileName;
            File.WriteAllLines(fileName, text, Encoding);
            if (string.IsNullOrEmpty(CurrentOpenedFile))
            {
                CurrentOpenedFile = fileName;
            }
        }

        public void SaveTextInOpenedFile(IEnumerable<string> text)
        {
            if (!string.IsNullOrEmpty(CurrentOpenedFile))
            {
                File.WriteAllLines(CurrentOpenedFile, text, Encoding);
            }
            else
            {
                SaveTextInNewFile(text);
            }
        }

        public IList<string> ReadTextFromFile()
        {
            var openFileDialog = new OpenFileDialog {Filter = Filter};
            if (openFileDialog.ShowDialog() != true || string.IsNullOrEmpty(openFileDialog.FileName))
                return null;
            CurrentOpenedFile = openFileDialog.FileName;
            return new List<string>(File.ReadAllLines(CurrentOpenedFile, Encoding));
        }
    }
}