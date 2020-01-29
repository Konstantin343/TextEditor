using TestStack.White.UIItems;

namespace TestTextEditor.Framework.Forms.MenuForms
{
    public class FileMenuForm : BaseMenuForm
    {
        public FileMenuForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }
        
        public void SelectItem(FileMenuItem item) => SelectItem(item.ToString());

        public void NewFile() => SelectItem(FileMenuItem.New);
        
        public void OpenFile() => SelectItem(FileMenuItem.Open);

        public void SaveFile() => SelectItem(FileMenuItem.Save);

        public void SaveAsFile() => SelectItem(FileMenuItem.SaveAs);
    }

    public enum FileMenuItem
    {
        New,
        Open,
        Save,
        SaveAs
    }
}