using System.Collections.Generic;

namespace TextEditor.Highlight
{
    public interface IHighlightService
    {
        ISet<string> WordsToHighlight { get; }
        
        void SetWordsToHighlight(string fileName);
    }
}