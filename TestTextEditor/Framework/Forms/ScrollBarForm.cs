using TestStack.White.UIItems;
using TestTools.Logger;

namespace TestTextEditor.Framework.Forms
{
    public class ScrollBarForm : BaseForm
    {
        public ScrollBarForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        public bool IsHorizontalDisplayed => ((Panel) _source).ScrollBars.Horizontal.IsScrollable;
        
        public bool IsVerticalDisplayed => ((Panel) _source).ScrollBars.Vertical.IsScrollable;

        public bool AreBothDisplayed => IsHorizontalDisplayed && IsVerticalDisplayed;

        public void ScrollToBegin()
        {
            TestLogger.Instance.Info($"Scroll both bars of '{_name}' to begin");
            ((Panel) _source).ScrollBars.Horizontal.SetToMinimum();
            ((Panel) _source).ScrollBars.Vertical.SetToMinimum();
        }
    }
}