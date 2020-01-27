﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace TextEditor.TextEditComponent.TextHelpers
{
    public class HighlightTextManager
    {
        public ISet<string> WordsToHighlight { get; set; }
        
        public Brush HighlightBrush { get; set; }

        public HighlightTextManager(IEnumerable<string> wordsToHighlight, Brush highlighter)
        {
            WordsToHighlight = new HashSet<string>(wordsToHighlight);
            HighlightBrush = highlighter;
        }

        public void HighlightText(FormattedText formattedText)
        {
            var words = Regex.Split(formattedText.Text, @"\W");
            var index = 0;
            foreach (var word in words)
            {
                if (WordsToHighlight.Contains(word))
                {
                    formattedText.SetForegroundBrush(HighlightBrush, index, word.Length);
                }

                index += word.Length + 1;
            }
        }
    }
}