using System.Windows;
using NUnit.Framework;
using TestStack.White.InputDevices;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WPFUIItems;
using TestTextEditor.Framework;
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

//        [Test]
//        [TestCaseSource(typeof(TextProviders), nameof(TextProviders.SelectTextProviders))]
//        public void SelectTest(
//            List<string> textToInsert,
//            int startStr, int startChr,
//            int endStr, int endChr,
//            string expectedText)
//        {
//            var textEditBox = MainWindow.TextEditBoxForm;
//            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
//            textEditBox.RightClick();
//
//            var contextMenu = MainWindow.ContextMenuForm;
//            contextMenu.Copy();
//            Assert.AreEqual(expectedText, ClipboardHelper.GetText());
//        }
    }
}