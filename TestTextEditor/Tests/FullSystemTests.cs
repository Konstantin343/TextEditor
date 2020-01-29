using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;

namespace TestTextEditor.Tests
{
    public class FullSystemTests : BaseTests
    {
        [Test]
        public void SystemTest1()
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("small.txt");
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_1.txt");
            var textToChange = new List<string>(new[] {"123213", "abcd", "  ee  e ee "});
            var (selectStartStr, selectStartChr, selectEndStr, selectEndChr) = (0, 5, 2, 10);
            var (pasteStr, pasteChr) = (2, 7);
            var (enterStr, enterChr) = (0, 5);

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            textEditBox.Select(selectStartStr, selectStartChr, selectEndStr, selectEndChr);
            textEditBox.RightClick();
            var contextMenu1 = MainWindow.ContextMenuForm;
            contextMenu1.Copy();
            var copiedText = TextHelper.GetTextInBounds(textEditBox.SplittedText, selectStartStr, selectStartChr,
                selectEndStr, selectEndChr);
            Assert.AreEqual(copiedText, ClipboardHelper.GetText(),
                "3. Copied text is not equal to expected");

            textEditBox.ClickAt(pasteStr, pasteChr);
            textEditBox.RightClick();
            var contextMenu2 = MainWindow.ContextMenuForm;
            var expectedText1 = TextHelper.InsertLinesInText(
                textEditBox.SplittedText,
                Regex.Split(copiedText, "\r\n"),
                pasteStr, pasteChr);
            contextMenu2.Paste();
            Assert.AreEqual(
                expectedText1,
                textEditBox.Text,
                "4. Copied text wasn't paste");

            SaveFileAs(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "5. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "6. Current opened file doesn't displayed");

            OpenFile(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "7. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave,
                "8. Current opened file doesn't displayed");

            var expectedText2 = TextHelper.InsertLinesInText(
                textEditBox.SplittedText,
                textToChange,
                enterStr, enterChr);
            textEditBox.ClickAt(enterStr, enterChr);
            textEditBox.EnterMultiLineText(textToChange);
            Assert.AreEqual(
                expectedText2,
                textEditBox.Text,
                "9. Text wasn't enter");

            SaveFile();
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "10. File wasn't save");
        }
    }
}