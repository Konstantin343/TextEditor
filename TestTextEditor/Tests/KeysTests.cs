using System.Collections.Generic;
using NUnit.Framework;
using TestStack.White.WindowsAPI;
using TestTextEditor.Tests.DataProviders;

namespace TestTextEditor.Tests
{
    public class KeysTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.BackspaceInLineProviders))]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.BackspaceInBeginProviders))]
        public void BackspaceTest(
            List<string> textToInsert,
            int strToClick,
            int chrToClick,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndClick(textEditBox, textToInsert, strToClick, chrToClick);
            textEditBox.PressBackspaceKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.DeleteInLineProviders))]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.DeleteInEndProviders))]
        public void DeleteTest(
            List<string> textToInsert,
            int strToClick,
            int chrToClick,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndClick(textEditBox, textToInsert, strToClick, chrToClick);
            textEditBox.PressDeleteKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.EnterProviders))]
        public void EnterTest(
            List<string> textToInsert,
            int strToClick,
            int chrToClick,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndClick(textEditBox, textToInsert, strToClick, chrToClick);
            textEditBox.PressEnterKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.DeleteBackspaceSelectedTextProviders))]
        public void BackspaceSelectedTest(
            List<string> textToInsert,
            int startStr, int startChr,
            int endStr, int endChr,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.PressBackspaceKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.DeleteBackspaceSelectedTextProviders))]
        public void DeleteSelectedTest(
            List<string> textToInsert,
            int startStr, int startChr,
            int endStr, int endChr,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.PressDeleteKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.EnterSelectedTextProviders))]
        public void EnterSelectedTest(
            List<string> textToInsert,
            int startStr, int startChr,
            int endStr, int endChr,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.PressEnterKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.TabProviders))]
        public void TabTest(
            List<string> textToInsert,
            int strToClick,
            int chrToClick,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndClick(textEditBox, textToInsert, strToClick, chrToClick);
            textEditBox.PressTabKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }
        
        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.TabSelectedTextProviders))]
        public void TabSelectedTest(
            List<string> textToInsert,
            int startStr, int startChr,
            int endStr, int endChr,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.PressTabKey();
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.InsertProviders))]
        public void InsertTest(
            List<string> textToInsert,
            string marker,
            int strToClick, int chrToClick,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndClick(textEditBox, textToInsert, strToClick, chrToClick);
            textEditBox.PressInsertKey();
            textEditBox.EnterOneLineText(marker);
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.UpArrowProviders))]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.DownArrowProviders))]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.LeftArrowProviders))]
        [TestCaseSource(typeof(KeysProviders), nameof(KeysProviders.RightArrowProviders))]
        public void ArrowsTest(
            List<string> textToInsert,
            string marker,
            int strToClick, int chrToClick,
            string expectedText,
            KeyboardInput.SpecialKeys arrow)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndClick(textEditBox, textToInsert, strToClick, chrToClick);
            textEditBox.PressKey(arrow);
            textEditBox.EnterOneLineText(marker);
            Assert.AreEqual(expectedText, textEditBox.Text);
        }
    }
}