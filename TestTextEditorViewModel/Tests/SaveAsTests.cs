using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TestTextEditorViewModel.DataProviders;
using TestTextEditorViewModel.TestData;
using TestTools.Utils;
using TextEditor.Highlight;

namespace TestTextEditorViewModel.Tests
{
    [TestFixture]
    public class SaveAsTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(FileProviders), nameof(FileProviders.FilesHighlightProvider))]
        public void SaveAsFileHighlightTest(
            string fileName,
            ISet<string> wordsToHighlight)
        {
            TestViewModel.SaveAsFile(fileName);
            CollectionAssert.AreEquivalent(wordsToHighlight, TestViewModel.WordsToHighlight,
                "Words to highlight is wrong");
        }

        [Test]
        [TestCaseSource(typeof(FileProviders), nameof(FileProviders.FilesProvider))]
        public void SaveAsFileCurrentOpenedTest(
            string fileName)
        {
            TestViewModel.SaveAsFile(fileName);
            Assert.AreEqual(fileName, TestViewModel.CurrentOpenedFile,
                "File name is wrong");
        }

        [Test]
        [TestCaseSource(typeof(FileProviders), nameof(FileProviders.FilesProvider))]
        public void SaveAsFileContentTest(
            string fileName)
        {
            TestViewModel.SetText(TextHelper.GetText());
            TestViewModel.SaveAsFile(fileName);
            Assert.AreEqual(File.ReadAllText(fileName), TestViewModel.Text,
                "Text is wrong");
        }

        [Test]
        public void SaveAsAfterOpenTest()
        {
            TestViewModel.OpenFile(BaseFiles.CsharpFile);
            CollectionAssert.AreEquivalent(BasicWordsToHighlight.CsWords, TestViewModel.WordsToHighlight,
                "1. Words to highlight is wrong");
            Assert.AreEqual(BaseFiles.CsharpFile, TestViewModel.CurrentOpenedFile,
                "1. File name is wrong");
            Assert.AreEqual(File.ReadAllText(BaseFiles.CsharpFile), TestViewModel.Text,
                "1. Text is wrong");

            TestViewModel.SaveAsFile(BaseFiles.ToSaveJavaFile);
            CollectionAssert.AreEquivalent(BasicWordsToHighlight.CsWords, TestViewModel.WordsToHighlight,
                "2. Words to highlight is wrong");
            Assert.AreEqual(BaseFiles.CsharpFile, TestViewModel.CurrentOpenedFile,
                "2. File name is wrong");
            Assert.AreEqual(File.ReadAllText(BaseFiles.ToSaveJavaFile), TestViewModel.Text,
                "2. Text is wrong");
        }

        [Test]
        public void SaveAsAfterSaveAsTest()
        {
            TestViewModel.SetText(TextHelper.GetText());
            TestViewModel.SaveAsFile(BaseFiles.ToSaveCsFile);
            CollectionAssert.AreEquivalent(BasicWordsToHighlight.CsWords, TestViewModel.WordsToHighlight,
                "1. Words to highlight is wrong");
            Assert.AreEqual(BaseFiles.ToSaveCsFile, TestViewModel.CurrentOpenedFile,
                "1. File name is wrong");
            Assert.AreEqual(File.ReadAllText(BaseFiles.ToSaveCsFile), TestViewModel.Text,
                "1. Text is wrong");

            TestViewModel.SaveAsFile(BaseFiles.ToSaveJavaFile);
            CollectionAssert.AreEquivalent(BasicWordsToHighlight.CsWords, TestViewModel.WordsToHighlight,
                "2. Words to highlight is wrong");
            Assert.AreEqual(BaseFiles.ToSaveCsFile, TestViewModel.CurrentOpenedFile,
                "2. File name is wrong");
            Assert.AreEqual(File.ReadAllText(BaseFiles.ToSaveJavaFile), TestViewModel.Text,
                "2. Text is wrong");
        }
    }
}