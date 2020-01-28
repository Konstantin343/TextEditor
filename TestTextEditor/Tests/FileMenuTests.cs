using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.DataProviders;

namespace TestTextEditor.Tests
{
    public class FileMenuTests : BaseTests
    {
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
            
            Assert.AreEqual(FileHelper.ReadAndDelete(filePath), textEditBox.Text);
        }
    }
}