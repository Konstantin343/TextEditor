using System;
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
        private readonly List<FormattedText> _formattedTextLines;

        private readonly TextLines _textLines;

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
            textLines.AddLineEvent += OnAddLines;
            textLines.ChangeTextEvent += OnChangeText;
            textLines.RemoveLineEvent += OnRemoveLines;
            textLines.UpdateLineEvent += OnUpdateLine;
            _textLines = textLines;
            FontSize = fontSize;
            FontStyle = fontStyle;
            TextBrush = textBrush;
            LineInterval = lineInterval;
            HighlightTextService = highlightTextService;
            _formattedTextLines = new List<FormattedText>();
        }

        public FormattedText this[int i]
        {
            get
            {
                if (_formattedTextLines[i] != null) return _formattedTextLines[i];
                _formattedTextLines[i] = GetFormatted(_textLines[i]);
                UpdateWidth();

                return _formattedTextLines[i];
            }
        }

        public void UpdateAll()
        {
            for (var i = 0; i < _formattedTextLines.Count; i++)
            {
                _formattedTextLines[i] = null;
            }
        }

        public void Decorate(TextDecorationCollection td, SelectedTextBounds selectionBounds, 
            int lowerBound, int upperBound)
        {
            var startStr = selectionBounds.RealStart.Str;
            var startNum = selectionBounds.RealStart.Chr;
            var endStr = selectionBounds.RealEnd.Str;
            var endNum = selectionBounds.RealEnd.Chr;

            for (var i = Math.Max(startStr, lowerBound); i <= Math.Min(endStr, upperBound); i++)
            {
                if (startStr == endStr && startStr == i)
                {
                    this[i].SetTextDecorations(td, startNum, endNum - startNum);
                }
                else if (startStr < i && i < endStr)
                {
                    this[i].SetTextDecorations(td, 0, this[i].Text.Length);
                }
                else if (startStr == i)
                {
                    this[i].SetTextDecorations(td, startNum, this[i].Text.Length - startNum);
                }
                else if (endStr == i)
                {
                    this[i].SetTextDecorations(td, 0, endNum);
                }
            }
        }

        public void Underline(SelectedTextBounds selectionBounds, int lowerBound, int upperBound) =>
            Decorate(TextDecorations.Underline, selectionBounds, lowerBound, upperBound);

        public void RemoveDecoration(SelectedTextBounds bounds, int lowerBound, int upperBound) =>
            Decorate(null, bounds, lowerBound, upperBound);

        private void OnUpdateLine(object sender, TextLineEventArgs e)
        {
            _formattedTextLines[e.Index] = null;
        }

        private void OnRemoveLines(object sender, TextLineEventArgs e)
        {
            _formattedTextLines.RemoveRange(e.Index, e.Count);
        }

        private void OnChangeText(object sender, TextLineEventArgs e)
        {
            var textLines = (TextLines) sender;
            _formattedTextLines.Clear();
            for (var i = 0; i < textLines.Count; i++)
            {
                _formattedTextLines.Add(null);
            }
        }

        private void OnAddLines(object sender, TextLineEventArgs e)
        {
            _formattedTextLines.InsertRange(e.Index, new FormattedText[e.Count]);
        }

        private FormattedText GetFormatted(string s)
        {
            var formattedLine = FormattedTextHelper.GetFormattedText(s, FontStyle, FontSize, TextBrush);
            HighlightTextService.HighlightText(formattedLine);
            return formattedLine;
        }

        private void UpdateWidth() =>
            MaxLineWidth = _formattedTextLines.Any()
                ? _formattedTextLines.Select(line => line?.WidthIncludingTrailingWhitespace).Max()
                  ?? 0d
                : 0d;
    }
}