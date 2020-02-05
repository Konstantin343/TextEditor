using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TextEditComponent.TextEditComponent.Helpers;
using TextEditComponent.TextEditComponent.Text;

namespace TextEditComponent.TextEditComponent.Services
{
    public class FormattedTextService
    {
        private IList<FormattedText> _formattedTextLines;

        public string FontStyle { get; set; }

        public double FontSize { get; set; }

        public Brush TextBrush { get; set; }

        public double LineInterval { get; set; }

        public double MaxLineWidth { get; private set; }

        public double TextHeight => _formattedTextLines.Count * (LineInterval + FontSize);

        public HighlightTextService HighlightTextService { get; set; }

        public FormattedTextService(
            TextLines textLines,
            string fontStyle,
            double fontSize,
            Brush textBrush,
            double lineInterval,
            HighlightTextService highlightTextService)
        {
            textLines.AddLineEvent += OnAddLine;
            textLines.ChangeTextEvent += OnChangeText;
            textLines.RemoveLineEvent += OnRemoveLine;
            textLines.UpdateLineEvent += OnUpdateLine;
            FontSize = fontSize;
            FontStyle = fontStyle;
            TextBrush = textBrush;
            LineInterval = lineInterval;
            HighlightTextService = highlightTextService;
            _formattedTextLines = new List<FormattedText>();
        }

        public FormattedText this[int i] => _formattedTextLines[i];
        
        public void UpdateAll()
        {
            for (var i = 0; i < _formattedTextLines.Count; i++)
            {
                _formattedTextLines[i] = GetFormatted(_formattedTextLines[i].Text);
            }
        }

        public void Decorate(TextDecorationCollection td, SelectedTextBounds bounds)
        {
            var startStr = bounds.RealStart.Str;
            var startNum = bounds.RealStart.Chr;
            var endStr = bounds.RealEnd.Str;
            var endNum = bounds.RealEnd.Chr;

            for (var i = startStr; i <= endStr; i++)
            {
                if (startStr == endStr && startStr == i)
                {
                    _formattedTextLines[i].SetTextDecorations(td, startNum, endNum - startNum);
                }
                else if (startStr < i && i < endStr)
                {
                    _formattedTextLines[i].SetTextDecorations(td, 0, _formattedTextLines[i].Text.Length);
                }
                else if (startStr == i)
                {
                    _formattedTextLines[i]
                        .SetTextDecorations(td, startNum, _formattedTextLines[i].Text.Length - startNum);
                }
                else if (endStr == i)
                {
                    _formattedTextLines[i].SetTextDecorations(td, 0, endNum);
                }
            }
        }

        public void Underline(SelectedTextBounds bounds) => Decorate(TextDecorations.Underline, bounds);

        public void RemoveDecoration(SelectedTextBounds bounds) => Decorate(null, bounds);

        private void OnUpdateLine(object sender, TextLineEventArgs e)
        {
            var textLines = (TextLines) sender;
            _formattedTextLines[e.Index] = GetFormatted(textLines[e.Index]);
            UpdateWidth();
        }

        private void OnRemoveLine(object sender, TextLineEventArgs e)
        {
            _formattedTextLines.RemoveAt(e.Index);
            UpdateWidth();
        }

        private void OnChangeText(object sender, TextLineEventArgs e)
        {
            var textLines = (TextLines) sender;
            _formattedTextLines.Clear();
            for (var i = 0; i < textLines.Count; i++)
            {
                _formattedTextLines.Add(GetFormatted(textLines[i]));
            }

            UpdateWidth();
        }

        private void OnAddLine(object sender, TextLineEventArgs e)
        {
            var textLines = (TextLines) sender;
            _formattedTextLines.Insert(e.Index, GetFormatted(textLines[e.Index]));
            UpdateWidth();
        }

        private FormattedText GetFormatted(string s)
        {
            var formattedLine = FormattedTextHelper.GetFormattedText(s, FontStyle, FontSize, TextBrush);
            HighlightTextService.HighlightText(formattedLine);
            return formattedLine;
        }

        private void UpdateWidth() =>
            MaxLineWidth = _formattedTextLines.Any()
                ? _formattedTextLines.Select(line => line.WidthIncludingTrailingWhitespace).Max()
                : 0d;
    }
}