using System.Collections.Generic;
using System.Windows;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.DataProviders;

namespace TestTextEditor.Tests
{
    public class ContextMenuTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.ContextMenuLocationProviders))]
        public void ContextMenuLocationTest(Point point)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.RightClickAt(point);
            Assert.AreEqual(textEditBox.GetAbsolutePoint(point), MainWindow.ContextMenuForm.Location);
        }

        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.SelectAllProviders))]
        public void SelectAllTest(IList<string> textToInsert)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterMultiLineText(textToInsert);

            textEditBox.RightClick();
            MainWindow.ContextMenuForm.SelectAll();

            textEditBox.RightClick();
            MainWindow.ContextMenuForm.Copy();

            Assert.AreEqual(textEditBox.Text, ClipboardHelper.GetText(textEditBox.Text));
        }

        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.CopyProviders))]
        public void CopyTest(
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
            Assert.AreEqual(expectedText, ClipboardHelper.GetText(expectedText));
        }

        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.CutProviders))]
        public void CutTest(
            IList<string> textToInsert,
            int startStr, int startChr,
            int endStr, int endChr,
            string expectedCopiedText,
            string expectedText)
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.RightClick();

            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Cut();
            Assert.AreEqual(expectedCopiedText, ClipboardHelper.GetText(expectedCopiedText));
            Assert.AreEqual(expectedText, textEditBox.Text);
        }

        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.PasteProviders))]
        public void PasteTest(
            IList<string> textToInsert,
            string textToPaste,
            int str, int chr,
            string expectedText)
        {
            ClipboardHelper.SetText(textToPaste);
            
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.EnterMultiLineText(textToInsert);
            textEditBox.ClickAt(str, chr);

            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Paste();
            
            Assert.AreEqual(expectedText, textEditBox.Text);
        }
    }
}