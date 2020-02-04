using Microsoft.Win32;

namespace TextEditor.FileDialog
{
    public class DialogService : IDialogService
    {
        public DialogService(string filter)
        {
            Filter = filter;
        }

        public string Filter { get; }

        public string OpenFileDialog() =>
            GetDialogText(
                new OpenFileDialog
                {
                    Filter = Filter,
                    Title = "Open File"
                });

        public string SaveFileDialog() =>
            GetDialogText(
                new SaveFileDialog
                {
                    Filter = Filter,
                    Title = "Save File As"
                });

        private static string GetDialogText(Microsoft.Win32.FileDialog fileDialog) =>
            fileDialog.ShowDialog() != true || string.IsNullOrEmpty(fileDialog.FileName)
                ? null
                : fileDialog.FileName;
    }
}