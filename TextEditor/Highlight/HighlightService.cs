using System;
using System.Collections.Generic;
using System.Linq;
using TextEditor.ViewModel;

namespace TextEditor.Highlight
{
    public class HighlightService : BaseNotifyPropertyChanged, IHighlightService
    {
        private ISet<string> _wordsToHighlight;

        public HighlightService(ISet<string> wordsToHighlight)
        {
            WordsToHighlight = wordsToHighlight;
        }

        public ISet<string> WordsToHighlight
        {
            get => _wordsToHighlight;
            set
            {
                _wordsToHighlight = value;
                OnPropertyChanged(nameof(WordsToHighlight));
            }
        }
        
        public void SetWordsToHighlight(string fileName) => 
            WordsToHighlight = GetWordsByLanguage(GetLanguageByName(fileName));

        private static ISet<string> GetWordsByLanguage(Language language)
        {
            switch (language)
            {
                case Language.Java:
                    return BasicWordsToHighlight.JavaWords;
                case Language.Cs:
                    return BasicWordsToHighlight.CsWords;
                default:
                    return BasicWordsToHighlight.CsWords;
            }
        }

        private static Language GetLanguageByName(
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