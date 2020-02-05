using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TextEditComponent.TextEditComponent.TextHelpers;

namespace TextEditComponent.TextEditComponent.Text
{
    public class TextLines
    {
        private IList<TextLine> _textLines;
        
        private string _fontStyle;
        private double _fontSize;
        private double _lineInterval;
        private Brush _textBrush;

        public string FontStyle
        {
            get => _fontStyle;
            set
            {
                _fontStyle = value;
                UpdateAll();
                UpdateWidth();
            }
        }

        public double FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                UpdateAll();
                UpdateWidth();
            }
        }

        public Brush TextBrush
        {
            get => _textBrush;
            set
            {
                _textBrush = value;
                UpdateAll();
                UpdateWidth();
            }
        }

        public double LineInterval
        {
            get => _lineInterval;
            set
            {
                _lineInterval = value;
                UpdateAll();
                UpdateWidth();
            }
        }

        public int Count => _textLines.Count;

        public HighlightTextService HighlightTextService { get; set; }

        public TextLines(
            IEnumerable<string> source,
            string fontStyle,
            double fontSize,
            Brush textBrush,
            double lineInterval,
            HighlightTextService highlightTextService)
        {
            _fontSize = fontSize;
            _fontStyle = fontStyle;
            _lineInterval = lineInterval;
            _textBrush = textBrush;
            HighlightTextService = highlightTextService;
            SetText(source);
        }

        public void SetText(IEnumerable<string> source)
        {
            _textLines = source.Select(CreateTextLine).ToList();
            UpdateWidth();
            UpdateAll();
        }

        public void SetHighlightWords(IEnumerable<string> source)
        {
            HighlightTextService.WordsToHighlight = new HashSet<string>(source);
            UpdateAll();
        }

        public double MaxLineWidth => _maxLineWidth;

        public double TextHeight => _textLines.Count * (LineInterval + FontSize);

        public void InsertLine(int index, string line)
        {
            if (index < 0 || index > _textLines.Count) return;
            _textLines.Insert(index, CreateTextLine(line));
            UpdateLine(index);
            UpdateWidth();
        }

        public void RemoveLineAt(int index)
        {
            if (index < 0 || index >= _textLines.Count) return;
            _textLines.RemoveAt(index);
            UpdateWidth();
        }

        public void InsertInLine(int lineIndex, string toInsert, int startIndex)
        {
            _textLines[lineIndex].Insert(toInsert, startIndex);
            UpdateLine(lineIndex);
            UpdateWidth();
        }

        public void RemoveInLine(int lineIndex, int startIndex, int count)
        {
            _textLines[lineIndex].Remove(startIndex, count);
            UpdateLine(lineIndex);
            UpdateWidth();
        }

        public void RemoveInLine(int lineIndex, int startIndex)
        {
            _textLines[lineIndex].Remove(startIndex);
            UpdateLine(lineIndex);
            UpdateWidth();
        }

        public string SubstringFromLine(int lineIndex, int startIndex, int count) =>
            _textLines[lineIndex].Substring(startIndex, count);

        public void AddInLine(int lineIndex, string toAdd)
        {
            _textLines[lineIndex].Add(toAdd);
            UpdateLine(lineIndex);
            UpdateWidth();
        }

        public string SubstringFromLine(int lineIndex, int startIndex) =>
            _textLines[lineIndex].Substring(startIndex);

        public TextLine this[int i] => _textLines[i];

        public IList<string> RawLines => _textLines.Select(tl => tl.RawValue).ToList();

        public void UpdateAll()
        {
            for (var i = 0; i < _textLines.Count; i++)
            {
                UpdateLine(i);
            }
        }

        internal void DeleteInBounds(SelectedTextBounds bounds)
        {
            var startStr = bounds.RealStart.Str;
            var startNum = bounds.RealStart.Chr;
            var endStr = bounds.RealEnd.Str;
            var endNum = bounds.RealEnd.Chr;

            if (bounds.IsOnOneLine)
            {
                _textLines[startStr].Remove(startNum, endNum - startNum);
                UpdateLine(startStr);
            }
            else
            {
                if (startNum != _textLines[startStr].Length)
                    _textLines[startStr].Remove(startNum);
                _textLines[startStr].Add(_textLines[endStr].Substring(endNum));
                UpdateLine(startStr);
                for (var i = 0; i < endStr - startStr; i++)
                {
                    RemoveLineAt(startStr + 1);
                }
            }
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

        internal void Decorate(TextDecorationCollection td, SelectedTextBounds bounds)
        {
            var startStr = bounds.RealStart.Str;
            var startNum = bounds.RealStart.Chr;
            var endStr = bounds.RealEnd.Str;
            var endNum = bounds.RealEnd.Chr;

            for (var i = startStr; i <= endStr; i++)
            {
                if (startStr == endStr && startStr == i)
                {
                    _textLines[i].Decorate(td, startNum, endNum - startNum);
                }
                else if (startStr < i && i < endStr)
                {
                    _textLines[i].Decorate(td, 0, _textLines[i].Length);
                }
                else if (startStr == i)
                {
                    _textLines[i].Decorate(td, startNum, _textLines[i].Length - startNum);
                }
                else if (endStr == i)
                {
                    _textLines[i].Decorate(td, 0, endNum);
                }
            }
        }

        internal void Underline(SelectedTextBounds bounds) => Decorate(TextDecorations.Underline, bounds);

        internal void RemoveDecoration(SelectedTextBounds bounds) => Decorate(null, bounds);

        private TextLine CreateTextLine(string line) =>
            new TextLine(line, FontStyle, FontSize, TextBrush, HighlightTextService);

        private double _maxLineWidth = 0d;

        private void UpdateWidth() =>
            _maxLineWidth = _textLines.Any()
                ? _textLines.Select(line => line.Width).Max()
                : 0d;

        private void UpdateLine(int index) =>
            _textLines[index].UpdateFormatted(FontStyle, FontSize, TextBrush, HighlightTextService);

        public override string ToString() => string.Join("\r\n", _textLines.Select(tl => tl.RawValue));
    }
}