using System.Windows;
using TestStack.White.UIItems;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.Framework.Forms
{
    public abstract class BaseForm
    {
        protected IUIItem _source;
        protected string _name;

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
        
        public void RightClickAt(Point relativePoint)
        {
            TestLogger.Instance.Info($"Right clicking on {_name}");
            _source.RightClickAt(GetAbsolutePoint(relativePoint));
        }

        public Point GetAbsolutePoint(Point relativePoint) => Location - new Point() + relativePoint;
    }
}