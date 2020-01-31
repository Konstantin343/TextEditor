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
                    return BasicWordsToHighlight.CsWords;
            }
        }

        public static Language GetLanguageByName(
            string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Language.None;
            }

            return Enum.TryParse<Language>(
                fileName.Split('.').Last(),
                true, out var language)
                ? language
                : Language.None;
        }
    }
}