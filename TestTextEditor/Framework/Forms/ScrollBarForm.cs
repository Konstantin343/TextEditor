using TestStack.White.UIItems;

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
    }
}