using System;
using System.Collections.Generic;
using System.Linq;

namespace TextEditor.Highlight
{
    public static class LanguageMapper
    {
        public static ISet<string> Map(
            this Language highlightedLanguages)
        {
            switch (highlightedLanguages)
            {
                case Language.Java:
                    return BasicWordsToHighlight.JavaWords;
                case Language.Cs:
                    return BasicWordsToHighlight.CsWords;
                default:
                    return BasicWordsToHighlight.NoWords;
            }
        }

        public static Language GetLanguageByName(
            string fileName) =>
            Enum.TryParse<Language>(
                fileName.Split('.').Last(),
                true, out var language)
                ? language
                : Language.None;
    }
}