using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TestTextEditor.Tests.DataProviders;
using TestTextEditor.Tests.TestData;
using TestTools.Utils;

namespace TestTextEditor.Tests
{
    public class FileMenuTests : BaseTests
    {
        [Test]
        public void SaveAfterOpenAppTest()
        {
            SaveFile();
            Assert.DoesNotThrow(() =>
            {
                var _ = MainWindow.SaveFileAsModalForm;
            });
        }

        [Test]
        public void OpenFileScrollBarsTest()
        {
            OpenFile(EnvironmentHelper.GetResourcePath("large.txt"));
            Assert.IsTrue(MainWindow.ScrollBarForm.AreBothDisplayed);
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.SaveAsCurrentOpenedFileProviders))]
        public void SaveAsCurrentOpenedFileTest(
            string filePath)
        {
            SaveFileAs(filePath);
            File.Delete(filePath);
            Assert.AreEqual(filePath, MainWindow.CurrentOpenedFile);
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.SaveAsProviders))]
        public void SaveAsTest(
            IList<string> textToInsert,
            string filePath)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterMultiLineText(textToInsert);
            SaveFileAs(filePath);
            Assert.AreEqual(textEditBox.Text, File.ReadAllText(filePath));
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.OpenFileProviders))]
        public void OpenCurrentOpenedFileTest(
            string filePath)
        {
            OpenFile(filePath);
            Assert.AreEqual(filePath, MainWindow.CurrentOpenedFile);
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.OpenFileProviders))]
        public void OpenTest(
            string filePath)
        {
            OpenFile(filePath);
            Assert.AreEqual(File.ReadAllText(filePath), MainWindow.TextEditBoxForm.Text);
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.SaveAfterSaveAsProviders))]
        public void SaveAfterSaveAsFileTest(
            string filePath,
            IList<string> textToInsert,
            IList<string> textToChange)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterMultiLineText(textToInsert);

            SaveFileAs(filePath);

            var textBeforeChanges = textEditBox.Text;
            textEditBox.EnterMultiLineText(textToChange);
            var fileAfterChanges = File.ReadAllText(filePath);

            SaveFile();

            Assert.AreEqual(textEditBox.Text, File.ReadAllText(filePath),
                "Text after save is not equal to expected");
            Assert.AreEqual(fileAfterChanges, textBeforeChanges,
                "Text before save is not equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.SaveAfterOpenProviders))]
        public void SaveAfterOpenFileTest(
            string filePath,
            IList<string> textToChange)
        {
            var textEditBox = MainWindow.TextEditBoxForm;

            OpenFile(filePath);

            var textBeforeChanges = textEditBox.Text;
            textEditBox.EnterMultiLineText(textToChange);
            var fileAfterChanges = File.ReadAllText(filePath);

            SaveFile();

            Assert.AreEqual(File.ReadAllText(filePath), textEditBox.Text,
                "Text after save is not equal to expected");
            Assert.AreEqual(fileAfterChanges, textBeforeChanges,
                "Text before save is not equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.SaveAsAfterSaveAsProviders))]
        public void SaveAsAfterSaveAsFileTest(
            string filePath1,
            string filePath2,
            IList<string> textToInsert,
            IList<string> textToChange)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterMultiLineText(textToInsert);

            SaveFileAs(filePath1);

            var textBeforeChanges = textEditBox.Text;
            textEditBox.EnterMultiLineText(textToChange);

            SaveFileAs(filePath2);

            var file1 = File.ReadAllText(filePath1);
            var file2 = File.ReadAllText(filePath2);

            Assert.AreEqual(file1, textBeforeChanges,
                "Text in first file is not equal to expected");
            Assert.AreEqual(file2, textEditBox.Text,
                "Text in second file is not equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.OpenAfterOpenProviders))]
        public void OpenAfterOpenFileTest(
            string filePath1,
            string filePath2)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(filePath1);
            var text = textEditBox.Text;
            var currentOpenedFile = MainWindow.CurrentOpenedFile;

            OpenFile(filePath2);

            Assert.AreEqual(File.ReadAllText(filePath1), text,
                "Text after first open is not equal to expected");
            Assert.AreEqual(filePath1, currentOpenedFile,
                "Current opened file after first open is not equal to expected");
            Assert.AreEqual(File.ReadAllText(filePath2), textEditBox.Text,
                "Text after second open save is not equal to expected");
            Assert.AreEqual(filePath2, MainWindow.CurrentOpenedFile,
                "Current opened file after second open is not equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.NewAfterOpenProviders))]
        public void NewAfterOpenFileTest(
            string filePath)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(filePath);
            Assert.AreEqual(File.ReadAllText(filePath), textEditBox.Text);
            NewFile();
            Assert.IsEmpty(textEditBox.Text);
            Assert.IsEmpty(MainWindow.CurrentOpenedFile);
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.NewAfterSaveAsProviders))]
        public void NewAfterSaveAsFileTest(
            string filePath)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterMultiLineText(BaseTestObjects.TextToInsertSelectedTests);
            SaveFileAs(filePath);
            Assert.AreEqual(File.ReadAllText(filePath), textEditBox.Text);
            NewFile();
            Assert.IsEmpty(textEditBox.Text);
            Assert.IsEmpty(MainWindow.CurrentOpenedFile);
        }
    }
}