using System.Collections;
using NUnit.Framework;
using TestTextEditorViewModel.TestData;
using TextEditor.Highlight;

namespace TestTextEditorViewModel.DataProviders
{
    public class FileProviders
    {
        public static IEnumerable ResourceFilesHighlightProvider
        {
            get
            {
                yield return new TestCaseData(BaseFiles.TextFile, BasicWordsToHighlight.CsWords)
                    .SetName("TxtFile_{m}");
                
                yield return new TestCaseData(BaseFiles.CsharpFile, BasicWordsToHighlight.CsWords)
                    .SetName("CsFile_{m}");
                
                yield return new TestCaseData(BaseFiles.JavaFile, BasicWordsToHighlight.JavaWords)
                    .SetName("JavaFile_{m}");
            }
        }
        
        public static IEnumerable ResourceFilesProvider
        {
            get
            {
                yield return new TestCaseData(BaseFiles.TextFile)
                    .SetName("TxtFile_{m}");
                
                yield return new TestCaseData(BaseFiles.CsharpFile)
                    .SetName("CsFile_{m}");
                
                yield return new TestCaseData(BaseFiles.JavaFile)
                    .SetName("JavaFile_{m}");
            }
        }
        
        public static IEnumerable FilesHighlightProvider
        {
            get
            {
                yield return new TestCaseData(BaseFiles.ToSaveTxtFile, BasicWordsToHighlight.CsWords)
                    .SetName("TxtFile_{m}");
                
                yield return new TestCaseData(BaseFiles.ToSaveCsFile, BasicWordsToHighlight.CsWords)
                    .SetName("CsFile_{m}");
                
                yield return new TestCaseData(BaseFiles.ToSaveJavaFile, BasicWordsToHighlight.JavaWords)
                    .SetName("JavaFile_{m}");
            }
        }
        
        public static IEnumerable FilesProvider
        {
            get
            {
                yield return new TestCaseData(BaseFiles.ToSaveTxtFile)
                    .SetName("TxtFile_{m}");
                
                yield return new TestCaseData(BaseFiles.ToSaveCsFile)
                    .SetName("CsFile_{m}");
                
                yield return new TestCaseData(BaseFiles.ToSaveJavaFile)
                    .SetName("JavaFile_{m}");
            }
        }
    }
}