using System.Linq;
using NUnit.Framework;
using TestStack.White.WindowsAPI;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.DataProviders;
using TestTextEditor.Tests.TestData;

namespace TestTextEditor.Tests
{
    public class FocusPositionTests : BaseTests
    {
        [Test]
        public void IsNotFocusedTest()
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            Assert.IsFalse(textEditBox.IsFocused);
        }

        [Test]
        public void IsFocusedByClickTest()
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.Click();
            Assert.IsTrue(textEditBox.IsFocused);
        }

        [Test]
        public void IsNotFocusedByRightClickTest()
        {
            var textEditBox = MainWindow.TextEditBoxForm;
            textEditBox.RightClick();
            Assert.IsFalse(textEditBox.IsFocused);
        }

        [Test]
        public void ClickAfterSelectTest()
        {
            var textToInsert = BaseTestObjects.TextToInsertSelectedTests;
            var (startStr, startChr, endStr, endChr) = BaseTestObjects.BoundsSelectedTests.FirstOrDefault();
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.Click();

            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Copy();
            Assert.IsEmpty(ClipboardHelper.GetText());
        }

        [Test]
        [TestCaseSource(typeof(FocusPositionProviders), nameof(FocusPositionProviders.ArrowsProvider))]
        public void ArrowsAfterSelectTest(
            KeyboardInput.SpecialKeys arrow)
        {
            var textToInsert = BaseTestObjects.TextToInsertSelectedTests;
            var (startStr, startChr, endStr, endChr) = BaseTestObjects.BoundsSelectedTests.FirstOrDefault();
            var textEditBox = MainWindow.TextEditBoxForm;
            EnterAndSelect(textEditBox, textToInsert, startStr, startChr, endStr, endChr);
            textEditBox.Click();

            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Copy();
            Assert.IsEmpty(ClipboardHelper.GetText());
        }
    }
}