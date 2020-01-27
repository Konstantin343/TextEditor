﻿using System.Windows;
using System.Windows.Media;
using TextEditor.TextEditComponent.TextHelpers;

namespace TextEditor.TextEditComponent.Text
{
    public class TextLine
    {
        private string _rawLine;
        private FormattedText _formattedLine;

        public TextLine(
            string line,
            string fontStyle,
            double fontSize,
            Brush textBrush,
            HighlightTextManager highlightTextManager)
        {
            SetValue(line);
            UpdateFormatted(fontStyle, fontSize, textBrush, highlightTextManager);
        }

        public void UpdateFormatted(
            string fontStyle,
            double fontSize,
            Brush textBrush,
            HighlightTextManager highlightTextManager)
        {
            _formattedLine = FormattedTextHelper.GetFormattedText(_rawLine, fontStyle, fontSize, textBrush);
            highlightTextManager.HighlightText(_formattedLine);
        }

        public void SetValue(string newValue) => _rawLine = newValue;

        public void Insert(string toInsert, int startIndex) => _rawLine = _rawLine.Insert(startIndex, toInsert);

        public void Remove(int startIndex, int count) => _rawLine = _rawLine.Remove(startIndex, count);
        public void Remove(int startIndex) => _rawLine = _rawLine.Remove(startIndex);

        public string Substring(int startIndex, int count) => _rawLine.Substring(startIndex, count);
        public string Substring(int startIndex) => _rawLine.Substring(startIndex);

        public void Add(string toAdd) => _rawLine += toAdd;


        public FormattedText FormattedValue => _formattedLine;
        public string RawValue => _rawLine;

        public string this[int i] => _rawLine[i].ToString();

        public double Width => _formattedLine.WidthIncludingTrailingWhitespace;

        public int Length => _rawLine.Length;

        public override string ToString() => _rawLine;

        public void Decorate(TextDecorationCollection td, int start, int count) =>
            _formattedLine.SetTextDecorations(td, start, count);
    }
}