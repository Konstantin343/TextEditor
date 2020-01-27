using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WPFUIItems;

namespace TestTextEditor.Framework.Forms
{
    public class ContextMenuForm : BaseForm
    {
        public ContextMenuForm(PopUpMenu uiItem, string name) : base(uiItem, name)
        {
        }

        private void SelectItem(ContextMenuItem item) =>
            _source.Get(SearchCriteria.ByAutomationId(item.ToString())).Click();

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