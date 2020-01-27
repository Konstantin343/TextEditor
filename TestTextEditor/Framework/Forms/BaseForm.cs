using System.Windows;
using TestStack.White.UIItems;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.Framework.Forms
{
    public class BaseForm
    {
        protected readonly IUIItem _source;
        protected readonly string _name;

        public BaseForm(IUIItem uiItem, string name)
        {
            _source = uiItem;
            _name = name;
        }

        public Point Location => _source.Location;

        public IUIItem Source => _source;

        public bool IsFocused => _source.IsFocussed;

        public string Name => _name;
        
        public void Click()
        {
            TestLogger.Instance.Info($"Clicking on {_name}");
            _source.Click();
        }

        public void RightClick()
        {
            TestLogger.Instance.Info($"Right clicking on {_name}");
            _source.RightClick();
        }
    }
}