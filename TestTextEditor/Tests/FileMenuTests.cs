using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.DataProviders;

namespace TestTextEditor.Tests
{
    public class FileMenuTests : BaseTests
    {
        [Test]
        public void SaveAfterOpenAppTest()
        {
            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.SaveFile();

            Assert.DoesNotThrow(() =>
            {
                var _ = MainWindow.SaveFileAsModalForm;
            });
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.SaveAsCurrentOpenedFileProviders))]
        public void SaveAsCurrentOpenedFileTest(
            string filePath)
        {
            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.SaveAsFile();

            var modalForm = MainWindow.SaveFileAsModalForm;
            modalForm.EnterText(filePath);
            modalForm.Submit();
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

            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.SaveAsFile();

            var modalForm = MainWindow.SaveFileAsModalForm;
            modalForm.EnterText(filePath);
            modalForm.Submit();

            Assert.AreEqual(textEditBox.Text, FileHelper.ReadAndDelete(filePath));
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.OpenFileProviders))]
        public void OpenCurrentOpenedFileTest(
            string filePath)
        {
            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.OpenFile();

            var modalForm = MainWindow.OpenFileModalForm;
            modalForm.EnterText(filePath);
            modalForm.Submit();

            Assert.AreEqual(filePath, MainWindow.CurrentOpenedFile);
        }

        [Test]
        [TestCaseSource(typeof(FileMenuProviders), nameof(FileMenuProviders.OpenFileProviders))]
        public void OpenTest(
            string filePath)
        {
            var textEditBox = MainWindow.TextEditBoxForm;

            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.OpenFile();

            var modalForm = MainWindow.OpenFileModalForm;
            modalForm.EnterText(filePath);
            modalForm.Submit();

            Assert.AreEqual(FileHelper.Read(filePath), textEditBox.Text);
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

            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.SaveAsFile();

            var saveFileAsModalForm = MainWindow.SaveFileAsModalForm;
            saveFileAsModalForm.EnterText(filePath);
            saveFileAsModalForm.Submit();

            var textBeforeChanges = textEditBox.Text;
            textEditBox.EnterMultiLineText(textToChange);
            var fileAfterChanges = FileHelper.Read(filePath);

            fileMenu.Click();
            fileMenu.SaveFile();

            Assert.AreEqual(FileHelper.ReadAndDelete(filePath), textEditBox.Text,
                "Text after save is not equal to expected");
            Assert.AreEqual(fileAfterChanges, textBeforeChanges,
                "Text before save is not equal to expected");
        }
    }
}