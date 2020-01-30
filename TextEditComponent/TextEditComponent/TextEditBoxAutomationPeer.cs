using System.Windows.Automation.Peers;

namespace TextEditComponent.TextEditComponent
{
    public class TextEditBoxAutomationPeer : FrameworkElementAutomationPeer
    {
        public TextEditBoxAutomationPeer(TextEditBox control)
            : base(control)
        {
        }

        protected override string GetClassNameCore() => "TextEditBox";

        protected override AutomationControlType GetAutomationControlTypeCore() => AutomationControlType.Text;

        protected override string GetNameCore() => ((TextEditBox) Owner).Text;

        public override object GetPattern(PatternInterface patternInterface) =>
            patternInterface == PatternInterface.Text ? this : base.GetPattern(patternInterface);

    }
}