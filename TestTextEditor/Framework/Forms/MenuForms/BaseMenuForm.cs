using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WPFUIItems;
using TestTools.Logger;

namespace TestTextEditor.Framework.Forms.MenuForms
{
    public abstract class BaseMenuForm : BaseForm
    {
        public BaseMenuForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }
        
        protected void SelectItem(string item)
        {
            TestLogger.Instance.Info($"Click on {item} in '{_name}'");
            _source.Get(SearchCriteria.ByAutomationId(item)).Click();
        }
    }
}