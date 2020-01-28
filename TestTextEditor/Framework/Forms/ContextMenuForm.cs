using System.Windows;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WPFUIItems;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.Framework.Forms
{
    public class ContextMenuForm : BaseForm
    {
        public ContextMenuForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        private void SelectItem(ContextMenuItem item)
        {
            TestLogger.Instance.Info($"Click on {item} in {_name}");
            _source.Get(SearchCriteria.ByAutomationId(item.ToString())).Click();
        }

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