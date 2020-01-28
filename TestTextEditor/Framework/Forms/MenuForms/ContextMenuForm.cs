using System.Windows;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WPFUIItems;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.Framework.Forms
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