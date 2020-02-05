using System.Collections.Generic;
using System.Linq;

namespace TextEditComponent.TextEditComponent.Text
{
    public class TextLines
    {
        public delegate void TextLineEventHandler(object sender, TextLineEventArgs e);
        public event TextLineEventHandler ChangeTextEvent;
        public event TextLineEventHandler AddLineEvent;
        public event TextLineEventHandler RemoveLineEvent;
        public event TextLineEventHandler UpdateLineEvent;

        private IList<string> _textLines;
        
        public int Count => _textLines.Count;
        public TextLines(IEnumerable<string> source) => SetText(source);

        public void SetText(IEnumerable<string> source)
        {
            _textLines = source.ToList();
            ChangeTextEvent?.Invoke(this, new TextLineEventArgs(0));
        }

        public void InsertLine(int index, string line)
        {
            if (index < 0 || index > _textLines.Count) return;
            _textLines.Insert(index, line);
            AddLineEvent?.Invoke(this, new TextLineEventArgs(index));
        }

        public void RemoveLineAt(int index)
        {
            if (index < 0 || index >= _textLines.Count) return;
            _textLines.RemoveAt(index);
            RemoveLineEvent?.Invoke(this, new TextLineEventArgs(index));
        }

        public void InsertInLine(int lineIndex, string toInsert, int startIndex)
        {
            _textLines[lineIndex] = _textLines[lineIndex].Insert(startIndex, toInsert);
            UpdateLineEvent?.Invoke(this, new TextLineEventArgs(lineIndex));
        }

        public void RemoveInLine(int lineIndex, int startIndex, int count)
        {
            _textLines[lineIndex] = _textLines[lineIndex].Remove(startIndex, count);
            UpdateLineEvent?.Invoke(this, new TextLineEventArgs(lineIndex));
        }

        public void RemoveInLine(int lineIndex, int startIndex)
        {
            _textLines[lineIndex] = _textLines[lineIndex].Remove(startIndex);
            UpdateLineEvent?.Invoke(this, new TextLineEventArgs(lineIndex));
        }

        public string SubstringFromLine(int lineIndex, int startIndex, int count) =>
            _textLines[lineIndex].Substring(startIndex, count);

        public void AddInLine(int lineIndex, string toAdd)
        {
            _textLines[lineIndex] += toAdd;
            UpdateLineEvent?.Invoke(this, new TextLineEventArgs(lineIndex));
        }

        public string this[int i] => _textLines[i];

        public IList<string> Lines => new List<string>(_textLines);

        internal void DeleteInBounds(SelectedTextBounds bounds)
        {
            var startStr = bounds.RealStart.Str;
            var startNum = bounds.RealStart.Chr;
            var endStr = bounds.RealEnd.Str;
            var endNum = bounds.RealEnd.Chr;

            if (bounds.IsOnOneLine)
            {
                _textLines[startStr] = _textLines[startStr].Remove(startNum, endNum - startNum);
            }
            else
            {
                if (startNum != _textLines[startStr].Length)
                    _textLines[startStr].Remove(startNum);
                _textLines[startStr] += _textLines[endStr].Substring(endNum);
                for (var i = 0; i < endStr - startStr; i++)
                {
                    RemoveLineAt(startStr + 1);
                }
            }
            UpdateLineEvent?.Invoke(this, new TextLineEventArgs(startStr));
        }

        internal string GetInBounds(SelectedTextBounds bounds)
        {
            var startStr = bounds.RealStart.Str;
            var startNum = bounds.RealStart.Chr;
            var endStr = bounds.RealEnd.Str;
            var endNum = bounds.RealEnd.Chr;

            string selectedText;
            if (bounds.IsOnOneLine)
            {
                selectedText = _textLines[endStr].Substring(startNum, endNum - startNum);
            }
            else
            {
                var firstLine = _textLines[startStr].Substring(startNum);
                var lastLine = _textLines[endStr].Substring(0, endNum);
                selectedText = endStr - startStr > 1
                    ? $"{firstLine}\r\n{string.Join("\r\n", _textLines.Where((s, i) => startStr < i && i < endStr))}\r\n{lastLine}"
                    : $"{firstLine}\r\n{lastLine}";
            }

            return selectedText;
        }
        
        public override string ToString() => string.Join("\r\n", _textLines);
    }
}