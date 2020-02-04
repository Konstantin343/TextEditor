namespace TextEditor.FileDialog
{
    public interface IDialogService
    {
        string Filter { get; }
        string OpenFileDialog();
        string SaveFileDialog();
    }
}