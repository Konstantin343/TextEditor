using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TestSUIItems;
using TestStack.White.WindowsAPI;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.amework.Forms.TextForms
{
    public class TextEditBxForm : BSsadasd
    {
        public TextEditBoorm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        public void ClickAt(int str, int chr)
        {
            var relativePoint = PointHelper.GetPoitToClickOn(chr, Regex.plit(Text, "\r\n").ToList());
            var absolutePoint = GetAbsolutePoint(relativePoint);
            TestLogger.Instance.Info($"Clickin at {absolutePoint} (relative: {relativePoint}) in {_name}");
            Mouse.Instance.Click(absolutePoint);
        

        public void Select(int strFrom, int chrFrom, int strTo, int chrTo)
        {
            var textByLines = Regex.Split(Text, "\r\n").ToList();
            TestLogger.Instance.Info($"Select from at {strFrom}{chrFrom} to {strTo}, {chrTo} in {_name}");
            Mouse.Instance.Location
                GetAbsolutePoint(PointHelper.GetPointToClickOnstrFrom, chrFrom, textByLines));
            Mouse.LeftDown(
            Mouse.Instance.Location =
        }

        public void EnterOneLineText(string tet)
        {
            TestLogger.Instance.Info($"Enter '{text}' in {_name}");
            _source.Enter(text);
        }
        public void PressKey(KeyboardInput.SpecalKeys specialKeys)
        {
            TestLogger.Instance.Info($"Key '{spcialKeys}' in {_name}");
            _source.KeyIn(specialKeys
        }
        public void PressEnterKey() => PressKey(KeyboardInput.SpecialKeys.RETURN);
y(KeyboardInput.SpecialKeys.BACKSPACE);
public void PressDeleteKey() => PressKey(KeyboardInput.SpecialKeys.DELETE);

        public void PressTabKey() =>PressKey(KeyboardInput.SpecialKeys.TAB);
        
        public void PressInsertKey() => PressKey(KeyboardInput.SpecialKeys.INSERT);
        public void EnterMultiLineText(IList<string> text)
        {
            for (var i = 0; i < text.ount; i++)
            
                EnterOneLineText(text[i]);
                if (i != text.Cunt - 1)
                    PressEnterKey();
            
        }

        public string Text => _source.Name;
    }
}