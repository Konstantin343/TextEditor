using System.Windows.Automation;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WPFUIItems;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.Framework.Forms.ModalForns
{
    public class ModalForm : BaseForm
    {
        public ModalForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        public void EnterText(string text)
        {
            TestLogger.Instance.Info($"Enter {text} in {_name}");
            _source.Get(SearchCriteria.ByAutomationId("1001")).Enter(text);
        }

        public void Submit()
        {
            TestLogger.Instance.Info($"Submit {_name}");
            _source.Get(SearchCriteria.ByControlType(ControlType.Button).AndAutomationId("1")).Click();
        }
    }
}