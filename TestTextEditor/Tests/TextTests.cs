using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.DataProviders;

namespace TestTextEditor.Tests
{
    [TestFixture]
    public class TextTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(TextProviders), nameof(TextProviders.OneLineTextProviders))]
        public void OneLineTest(string text)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterOneLineText(text);
            Assert.AreEqual(text, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(TextProviders), nameof(TextProviders.MultiLineTextProviders))]
        public void MultiLineTest(List<string> textToInsert, string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterMultiLineText(textToInsert);
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(TextProviders), nameof(TextProviders.InsertTextProviders))]
        public void InsertTextTest(
            IList<string> textToInsert,
            string textToChange,
            int str, int chr,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndClick(textEditBox, textToInsert, str, chr);
            textEditBox.EnterOneLineText(textToChange);
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(TextProviders), nameof(TextProviders.SelectTextProviders))]
        public void SelectTest(
            IList<string> textToInsert,
            int startStr, int startChr,
            int endStr, int endChr,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.RightClick();

            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Copy();
            Assert.AreEqual(expectedText, ClipboardHelper.GetText());
        }

        [Test]
        [TestCaseSource(typeof(TextProviders), nameof(TextProviders.ChangeSelectedTextProviders))]
        public void ChangeSelectedTest(
            IList<string> textToInsert,
            string textToChange,
            int startStr, int startChr,
            int endStr, int endChr,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.EnterOneLineText(textToChange);
            Assert.AreEqual(expectedText, textEditBox.Text);
        }
    }
}