using NUnit.Framework;
using TestTextEditorViewModel.TestData;
using TestTools.Utils;
using TextEditor.Highlight;

namespace TestTextEditorViewModel.Tests
{
    [TestFixture]
    public class NewFileTests : BaseTests
    {
        [Test]
        public void CurrentOpenedFileNewFileAfterOpen()
        {
            TestViewModel.OpenFile(BaseFiles.TextFile);
            TestViewModel.NewFile();
            AssertAll();
        }

        [Test]
        public void CurrentOpenedFileNewFileAfterSaveAs()
        {
            TestViewModel.SaveAsFile(BaseFiles.ToSaveAsFile);
            TestViewModel.NewFile();
            AssertAll();
        }

        [Test]
        public void CurrentOpenedFileNewFileSetText()
        {
            TestViewModel.SetText(TextHelper.GetText(10, 10));
            TestViewModel.NewFile();
            AssertAll();
        }

        private void AssertAll()
        {
            Assert.IsEmpty(TestViewModel.CurrentOpenedFile,
                "Current opened file is wrong");
            Assert.IsEmpty(TestViewModel.Text,
                "Text is wrong");
            Assert.AreEqual(BasicWordsToHighlight.CsWords, TestViewModel.WordsToHighlight,
                "Words to highlight is wrong");
        }
    }
}