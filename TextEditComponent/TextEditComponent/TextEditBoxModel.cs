using System;
using System.Collections.Generic;
using System.Linq;
using TextEditComponent.TextEditComponent.Text;

namespace TextEditComponent.TextEditComponent
{
    public class TextEditBoxModel
    {
        public TextLines TextLines { get; private set; }

        public SelectedTextBounds SelectedText { get; private set; }

        public TextPosition CurrentPosition { get; private set; }

        public int CurrentString => CurrentPosition.Str;

        public int CurrentChar => CurrentPosition.Chr;

        public string Text => TextLines.ToString();

        public bool IsInsertMode { get; set; }

        public TextEditBoxModel()
        {
            TextLines = new TextLines(new[] {""});
            SelectedText = new SelectedTextBounds();
            CurrentPosition = new TextPosition();
        }

        public void SetCurrentPosition(TextPosition textPosition)
        {
            SelectedText.SetBounds(textPosition);
            CurrentPosition = new TextPosition(textPosition);
        }

        public void SelectToPosition(TextPosition textPosition)
        {
            SelectedText.MouseSelectionEnd = new TextPosition(textPosition);
            CurrentPosition = new TextPosition(textPosition);
        }

        public bool DeleteSelectedText()
        {
            if (SelectedText.IsEmpty)
                return false;

            TextLines.DeleteInBounds(SelectedText);
            CurrentPosition = new TextPosition(SelectedText.RealStart);
            SelectedText.Invalidate();
            return true;
        }

        public void DeleteAfterCurrentPosition()
        {
            if (CurrentChar == TextLines[CurrentString].Length)
            {
                if (CurrentString == TextLines.Count - 1) return;
                var nextString = CurrentString + 1;
                TextLines.AddInLine(CurrentString, TextLines[nextString]);
                TextLines.RemoveLineAt(nextString);
            }
            else
            {
                TextLines.RemoveInLine(CurrentString, CurrentChar, 1);
            }
        }

        public void DeleteBeforeCurrentPosition()
        {
            if (CurrentChar == 0)
            {
                if (CurrentString == 0) return;
                var newPosition = TextLines[CurrentString - 1].Length;
                TextLines.AddInLine(CurrentString - 1, TextLines[CurrentString]);
                TextLines.RemoveLineAt(CurrentString);
                CurrentPosition.Str--;
                CurrentPosition.Chr = newPosition;
            }
            else
            {
                TextLines.RemoveInLine(CurrentString, CurrentChar - 1, 1);
                CurrentPosition.Chr--;
            }
        }

        public void SetPositionOneLineDown()
        {
            if (CurrentString == TextLines.Count - 1) return;
            CurrentPosition.Str++;
            CurrentPosition.Chr = Math.Min(CurrentChar, TextLines[CurrentString].Length);
        }

        public void SetPositionOneLineUp()
        {
            if (CurrentString == 0) return;
            CurrentPosition.Str--;
            CurrentPosition.Chr = Math.Min(CurrentChar, TextLines[CurrentString].Length);
        }

        public void SetPositionOneLineRight()
        {
            if (CurrentChar == TextLines[CurrentString].Length)
            {
                if (CurrentString == TextLines.Count - 1) return;
                CurrentPosition.Str++;
                CurrentPosition.Chr = 0;
                return;
            }

            CurrentPosition.Chr++;
        }

        public void SetPositionOneLineLeft()
        {
            if (CurrentChar == 0)
            {
                if (CurrentString == 0) return;
                CurrentPosition.Str--;
                CurrentPosition.Chr = TextLines[CurrentString].Length;
                return;
            }

            CurrentPosition.Chr--;
        }

        public void ChangeInsertMode() => IsInsertMode = !IsInsertMode;

        public void NewLineFromCurrentPosition()
        {
            TextLines.InsertLine(
                CurrentString + 1,
                CurrentChar < TextLines[CurrentString].Length
                    ? TextLines[CurrentString].Substring(CurrentChar)
                    : string.Empty);
            if (CurrentChar < TextLines[CurrentString].Length)
                TextLines.RemoveInLine(CurrentString, CurrentChar);
            CurrentPosition.Str++;
            CurrentPosition.Chr = 0;
        }

        public void AddTabulationOnCurrentPosition()
        {
            TextLines.InsertInLine(CurrentString, "\t", CurrentChar);
            CurrentPosition.Chr++;
        }

        public void AddTextOnCurrentPosition(string text)
        {
            if (IsInsertMode && CurrentChar < TextLines[CurrentString].Length)
                TextLines.RemoveInLine(CurrentString, CurrentChar, text.Length);
            TextLines.InsertInLine(CurrentString, text, CurrentChar);
            CurrentPosition.Chr += text.Length;
        }

        public void AddLinesOnCurrentPosition(IList<string> lines)
        {
            for (var i = 0; i < lines.Count; i++)
            {
                AddTextOnCurrentPosition(lines[i]);
                if (i != lines.Count - 1)
                    NewLineFromCurrentPosition();
            }
        }

        public void UpdateAll()
        {
            SelectedText.Invalidate();
            CurrentPosition = new TextPosition();
        }
    }
}