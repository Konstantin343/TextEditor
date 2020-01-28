using TestStack.White.UIItems;

namespace TestTextEditor.Framework.Forms.MenuForms
{
    public class ContextMenuForm : BaseMenuForm
    {
        public ContextMenuForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        public void SelectItem(ContextMenuItem item) => SelectItem(item.ToString());
        
        public void SelectAll() => SelectItem(ContextMenuItem.SelectAll);

        public void Copy() => SelectItem(ContextMenuItem.Copy);

        public void Paste() => SelectItem(ContextMenuItem.Paste);

        public void Cut() => SelectItem(ContextMenuItem.Cut);
    }

    public enum ContextMenuItem
    {
        Copy,
        Paste,
        Cut,
        SelectAll,
    }
}