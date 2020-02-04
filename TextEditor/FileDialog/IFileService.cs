using System.Collections.Generic;
using System.Text;

namespace TextEditor.FileDialog
{
    public interface IFileService
    {
        Encoding Encoding { get; }
        
        string CurrentOpenedFile { get; }
        
        List<string> OpenNewFile(string fileName);
        
        void SaveTextInFile(string fileName, IEnumerable<string> text);

        void SaveTextInCurrentFile(IEnumerable<string> text);

        void SaveAndCreateNewFile(IEnumerable<string> text);
    }
}