using System.Collections.Generic;
using TestTools.Logger;
using TextEditComponent.TextEditComponent;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Models
{
    public class TestTextEditBoxModel
    {
        private TextEditBoxModel TextEditBoxModel { get; }

        public TestTextEditBoxModel() => TextEditBoxModel = new TextEditBoxModel();

        public void AddLine(string textLine)
        {
            TestLogger.Instance.Info($"Enter line in Model:\r\n'{textLine}'");
            TextEditBoxModel.AddTextOnCurrentPosition(textLine);
        }

        public void AddLines(IList<string> textLines)
        {
            TestLogger.Instance.Info($"Enter lines in Model:\r\n'{string.Join("\r\n", textLines)}'");
            TextEditBoxModel.AddLinesOnCurrentPosition(textLines);
        }

        public void SelectText(SelectedTextBounds bounds)
        {
            TestLogger.Instance.Info($"Select text in Model:\r\n'{bounds}'");
            TextEditBoxModel.SetCurrentPosition(bounds.RealStart);
            TextEditBoxModel.SelectToPosition(bounds.RealEnd);
        }

        public void SetPositionTo(TextPosition position)
        {
            TestLogger.Instance.Info($"Set position in Model:\r\n'{position}'");
            TextEditBoxModel.SetCurrentPosition(position);
        }

        public void OneLineDown()
        {
            TestLogger.Instance.Info("Set position down on one line");
            TextEditBoxModel.SetPositionOneLineDown();
            TestLogger.Instance.Info($"Now is '{TextEditBoxModel.CurrentPosition}'");
        }

        public void OneLineUp()
        {
            TestLogger.Instance.Info("Set position up on one line");
            TextEditBoxModel.SetPositionOneLineUp();
            TestLogger.Instance.Info($"Now is '{TextEditBoxModel.CurrentPosition}'");
        }

        public void OneCharLeft()
        {
            TestLogger.Instance.Info("Set position left on one char");
            TextEditBoxModel.SetPositionOneCharLeft();
            TestLogger.Instance.Info($"Now is '{TextEditBoxModel.CurrentPosition}'");
        }

        public void OneCharRight()
        {
            TestLogger.Instance.Info("Set position right on one char");
            TextEditBoxModel.SetPositionOneCharRight();
            TestLogger.Instance.Info($"Now is '{TextEditBoxModel.CurrentPosition}'");
        }

        public void NewLine()
        {
            TestLogger.Instance.Info($"Add new line on current position:\r\n'{TextEditBoxModel.CurrentPosition}'");
            TextEditBoxModel.NewLineFromCurrentPosition();
        }
        
        public void Tabulation()
        {
            TestLogger.Instance.Info($"Add tabulation on current position:\r\n'{TextEditBoxModel.CurrentPosition}'");
            TextEditBoxModel.AddTabulationOnCurrentPosition();
        }

        public void ChangeInsertMode()
        {
            TestLogger.Instance.Info("Change insert mode");
            TextEditBoxModel.ChangeInsertMode();
            var state = TextEditBoxModel.IsInsertMode ? "on" : "over";
            TestLogger.Instance.Info($"Now is '{state}'");
        }
        
        public void DeleteSelected()
        {
            TestLogger.Instance.Info($"Delete selected in bounds:\r\n'{TextEditBoxModel.SelectedText}'");
            TextEditBoxModel.DeleteSelectedText();
        }
        
        public void DeleteBefore()
        {
            TestLogger.Instance.Info($"Delete before current position:\r\n'{TextEditBoxModel.CurrentPosition}'");
            TextEditBoxModel.DeleteBeforeCurrentPosition();
        }
        
        public void DeleteAfter()
        {
            TestLogger.Instance.Info($"Delete after current position:\r\n'{TextEditBoxModel.CurrentPosition}'");
            TextEditBoxModel.DeleteAfterCurrentPosition();
        }
        
        public TextPosition CurrentPosition
        {
            get
            {
                TestLogger.Instance.Info($"Got current position from Model:\r\n'{TextEditBoxModel.CurrentPosition}'");
                return TextEditBoxModel.CurrentPosition;
            }
        }

        public SelectedTextBounds SelectedTextBounds
        {
            get
            {
                TestLogger.Instance.Info(
                    $"Got selected text bounds position from Model:\r\n'{TextEditBoxModel.SelectedText}'");
                return TextEditBoxModel.SelectedText;
            }
        }

        public string FirstLine
        {
            get
            {
                TestLogger.Instance.Info($"Got first line from Model:\r\n'{TextEditBoxModel.TextLines[0]}'");
                return TextEditBoxModel.TextLines[0];
            }
        }

        public IList<string> TextLines
        {
            get
            {
                TestLogger.Instance.Info(
                    $"Got lines from Model:\r\n'{string.Join("\r\n", TextEditBoxModel.TextLines.Lines)}'");
                return TextEditBoxModel.TextLines.Lines;
            }
        }

        public bool IsInsertMode
        {
            get
            {
                var state = TextEditBoxModel.IsInsertMode ? "on" : "over";
                TestLogger.Instance.Info($"Insert mode is {state}");
                return TextEditBoxModel.IsInsertMode;
            }
        }
    }
}