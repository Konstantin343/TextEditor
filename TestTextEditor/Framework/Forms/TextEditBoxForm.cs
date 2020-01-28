using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TestStack.White.InputDevices;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WPFUIItems;
using TestStack.White.WindowsAPI;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.Framework.Forms
{
    public class TextEditBoxForm : BaseForm
    {
        public TextEditBoxForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        public void ClickAt(int str, int chr)
        {
            var relativePoint = PointHelper.GetPointToClickOn(str, chr, Text.Split('\n').ToList());
            var absolutePoint = GetAbsolutePoint(relativePoint);
            var b = _source.AutomationElement.Current.BoundingRectangle;
            TestLogger.Instance.Info($"Clicking at {absolutePoint} (relative: {relativePoint}) in {_name}");
            Mouse.Instance.Click(absolutePoint);
        }

        public void Select(int strFrom, int chrFrom, int strTo, int chrTo)
        {
            var textByLines = Text.Split('\n').ToList();
            TestLogger.Instance.Info($"Select from at {strFrom}, {chrFrom} to {strTo}, {chrTo} in {_name}");
            Mouse.Instance.Location =
                GetAbsolutePoint(PointHelper.GetPointToClickOn(strFrom, chrFrom, textByLines));
            Mouse.LeftDown();
            Mouse.Instance.Location =
                GetAbsolutePoint(PointHelper.GetPointToClickOn(strTo, chrTo, textByLines));
            Mouse.LeftUp();
        }

        public void EnterOneLineText(string text)
        {
            TestLogger.Instance.Info($"Enter '{text}' in {_name}");
            _source.Enter(text);
        }

        public void PressKey(KeyboardInput.SpecialKeys specialKeys)
        {
            TestLogger.Instance.Info($"Key '{specialKeys}' in {_name}");
            _source.KeyIn(specialKeys);
        }

        public void PressEnterKey() => PressKey(KeyboardInput.SpecialKeys.RETURN);

        public void PressBackspaceKey() => PressKey(KeyboardInput.SpecialKeys.BACKSPACE);

        public void PressDeleteKey() => PressKey(KeyboardInput.SpecialKeys.DELETE);

        public void PressTabKey() => PressKey(KeyboardInput.SpecialKeys.TAB);
        
        public void PressInsertKey() => PressKey(KeyboardInput.SpecialKeys.INSERT);

        public void EnterMultiLineText(IList<string> text)
        {
            for (var i = 0; i < text.Count; i++)
            {
                EnterOneLineText(text[i]);
                if (i != text.Count - 1)
                    PressEnterKey();
            }
        }

        public string Text => _source.Name;
    }
}