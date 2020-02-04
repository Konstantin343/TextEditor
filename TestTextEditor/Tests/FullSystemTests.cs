using System;
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
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Copy();
            var copiedText = TextHelper.GetTextInBounds(textEditBox.SplittedText, selectStartStr, selectStartChr,
                selectEndStr, selectEndChr);
            Assert.AreEqual(copiedText, ClipboardHelper.GetText(copiedText),
                "3. Copied text is not equal to expected");

            textEditBox.ClickAt(pasteStr, pasteChr);
            textEditBox.RightClick();
            var expectedText1 = TextHelper.InsertLinesInText(
                textEditBox.SplittedText,
                Regex.Split(copiedText, "\r\n"),
                pasteStr, pasteChr);
            contextMenu.Paste();
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

        [Test]
        public void SystemTest2()
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("small.txt");
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_2.txt");
            var (selectStartStr, selectStartChr, selectEndStr, selectEndChr) = (1, 2, 2, 20);
            var (pasteStr, pasteChr) = (0, 0);

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            textEditBox.Select(selectStartStr, selectStartChr, selectEndStr, selectEndChr);
            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            var copiedText = TextHelper.GetTextInBounds(textEditBox.SplittedText, selectStartStr, selectStartChr,
                selectEndStr, selectEndChr);
            var remainingText = TextHelper.GetTextNotInBounds(textEditBox.SplittedText, selectStartStr, selectStartChr,
                selectEndStr, selectEndChr);
            contextMenu.Cut();
            Assert.AreEqual(copiedText, ClipboardHelper.GetText(copiedText),
                "3. Copied text is not equal to expected");
            Assert.AreEqual(remainingText, textEditBox.Text,
                "4. Remaining text is not equal to expected");

            textEditBox.ClickAt(pasteStr, pasteChr);
            textEditBox.RightClick();
            var expectedText1 = TextHelper.InsertLinesInText(
                textEditBox.SplittedText,
                Regex.Split(copiedText, "\r\n"),
                pasteStr, pasteChr);
            contextMenu.Paste();
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

            textEditBox.ClickAt(pasteStr, pasteChr);
            textEditBox.RightClick();
            var expectedText2 = TextHelper.InsertLinesInText(
                textEditBox.SplittedText,
                Regex.Split(copiedText, "\r\n"),
                pasteStr, pasteChr);
            contextMenu.Paste();
            Assert.AreEqual(
                expectedText2,
                textEditBox.Text,
                "9. Copied text wasn't paste");

            SaveFile();
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "10. File wasn't save");
        }

        [Test]
        public void SystemTest3()
        {
            var fileToSave1 = EnvironmentHelper.GetOutputPath("system_test_3_1.txt");
            var fileToSave2 = EnvironmentHelper.GetOutputPath("system_test_3_2.txt");
            var fileToSave3 = EnvironmentHelper.GetOutputPath("system_test_3_3.txt");

            var text1 = "111111111111111";
            var text2 = "222222222222222";
            var text3 = "333333333333333";

            var textEditBox = MainWindow.TextEditBoxForm;

            textEditBox.EnterOneLineText(text1);
            SaveFileAs(fileToSave1);
            Assert.AreEqual(File.ReadAllText(fileToSave1), textEditBox.Text,
                "1. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave1,
                "2. Current opened file doesn't displayed");

            NewFile();
            Assert.IsEmpty(textEditBox.Text, "3. New file wasn't created");
            Assert.IsEmpty(MainWindow.CurrentOpenedFile, "4. Current opened file is not empty");

            textEditBox.EnterOneLineText(text2);
            SaveFileAs(fileToSave2);
            Assert.AreEqual(File.ReadAllText(fileToSave2), textEditBox.Text,
                "5. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave2,
                "6. Current opened file doesn't displayed");

            NewFile();
            Assert.IsEmpty(textEditBox.Text, "7. New file wasn't created");
            Assert.IsEmpty(MainWindow.CurrentOpenedFile, "8. Current opened file is not empty");

            textEditBox.EnterOneLineText(text3);
            SaveFileAs(fileToSave3);
            Assert.AreEqual(File.ReadAllText(fileToSave3), textEditBox.Text,
                "9. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave3,
                "10. Current opened file doesn't displayed");

            NewFile();
            Assert.IsEmpty(textEditBox.Text, "11. New file wasn't created");
            Assert.IsEmpty(MainWindow.CurrentOpenedFile, "12. Current opened file is not empty");
        }

        [Test]
        public void SystemTest4()
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("small.txt");
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_4.txt");

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.SelectAll();
            textEditBox.RightClick();
            contextMenu.Copy();
            Assert.AreEqual(textEditBox.Text, ClipboardHelper.GetText(textEditBox.Text),
                "3. Copied text is not equal to expected");

            NewFile();
            Assert.IsEmpty(textEditBox.Text, "4. New file wasn't created");
            Assert.IsEmpty(MainWindow.CurrentOpenedFile, "5. Current opened file is not empty");

            textEditBox.RightClick();
            contextMenu.Paste();
            Assert.AreEqual(
                ClipboardHelper.GetText(textEditBox.Text),
                textEditBox.Text,
                "6. Copied text wasn't paste");

            SaveFileAs(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "7. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave,
                "8. Current opened file doesn't displayed");

            OpenFile(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "9. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave,
                "10. Current opened file doesn't displayed");

            textEditBox.RightClick();
            contextMenu.SelectAll();
            textEditBox.PressDeleteKey();
            Assert.IsEmpty(
                textEditBox.Text,
                "11. Text in text box isn't empty");

            SaveFile();
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "12. File wasn't save");
        }

        [Test]
        public void SystemTest5()
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("clojure.clj");
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_5.txt");

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.SelectAll();
            textEditBox.RightClick();
            var copiedText = textEditBox.Text;
            contextMenu.Cut();
            Assert.AreEqual(copiedText, ClipboardHelper.GetText(copiedText),
                "3. Copied text is not equal to expected");
            Assert.IsEmpty(textEditBox.Text,
                "4. Text wasn't cut");
            
            textEditBox.RightClick();
            contextMenu.Paste();
            
            var expectedPastedText = textEditBox.Text;
            Assert.AreEqual(
                ClipboardHelper.GetText(expectedPastedText),
                expectedPastedText,
                "5. Copied text wasn't paste");

            SaveFileAs(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "6. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "7. Current opened file doesn't displayed");

            OpenFile(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "8. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave,
                "9. Current opened file doesn't displayed");

            var expectedText = TextHelper.InsertLinesInText(
                textEditBox.SplittedText, textEditBox.SplittedText, 0, 0);
            textEditBox.RightClick();
            contextMenu.Paste();
            Assert.AreEqual(
                expectedText,
                textEditBox.Text,
                "10. Text wasn't paste");

            SaveFile();
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "11. File wasn't save");
        }

        [Test]
        public void SystemTest6()
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("large.txt");
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_6.txt");
            var textToChange = new List<string>(new[] {"123213", "abcd", "  ee  e ee "});
            var (enterStr, enterChr) = (2, 3);

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.SelectAll();
            textEditBox.RightClick();
            var copiedText = textEditBox.Text;
            contextMenu.Cut();
            Assert.AreEqual(copiedText, ClipboardHelper.GetText(copiedText),
                "3. Copied text is not equal to expected");
            Assert.IsEmpty(textEditBox.Text,
                "4. Text wasn't cut");

            textEditBox.RightClick();
            contextMenu.Paste();
            Assert.AreEqual(
                ClipboardHelper.GetText(textEditBox.Text),
                textEditBox.Text,
                "5. Copied text wasn't paste");

            SaveFileAs(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "6. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "7. Current opened file doesn't displayed");

            OpenFile(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "8. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave,
                "9. Current opened file doesn't displayed");

            MainWindow.ScrollBarForm.ScrollToBegin();
            var expectedText = TextHelper.InsertLinesInText(
                textEditBox.SplittedText, textToChange, enterStr, enterChr);
            textEditBox.ClickAt(enterStr, enterChr);
            textEditBox.EnterMultiLineText(textToChange);
            Assert.AreEqual(
                expectedText,
                textEditBox.Text,
                "10. Text wasn't enter");

            SaveFile();
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "11. File wasn't save");
        }

        [Test]
        public void SystemTest7()
        {
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_7.txt");
            var textToEnter = new List<string>(new[] {"123213", "abcd", "  ee  e ee "});
            var (enterStr, enterChr) = (2, 11);

            var textEditBox = MainWindow.TextEditBoxForm;

            ClipboardHelper.SetText(string.Join("\r\n", textToEnter));
            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Paste();
            Assert.AreEqual(
                ClipboardHelper.GetText(textEditBox.Text),
                textEditBox.Text,
                "1. Copied text wasn't paste");

            SaveFileAs(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "2. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave,
                "3. Current opened file doesn't displayed");

            var expectedText = TextHelper.InsertLinesInText(textToEnter, textToEnter, enterStr, enterChr);
            textEditBox.ClickAt(enterStr, enterChr);
            textEditBox.EnterMultiLineText(textToEnter);
            Assert.AreEqual(
                expectedText,
                textEditBox.Text,
                "4. Text wasn't enter");

            SaveFile();
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "5. File wasn't save");
        }

        [Test]
        public void SystemTest8()
        
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("small.txt");
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_8.txt");
            var (clickStr, clickChr) = (0, 0);

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.SelectAll();
            textEditBox.RightClick();
            contextMenu.Copy();
            Assert.AreEqual(textEditBox.Text, ClipboardHelper.GetText(textEditBox.Text),
                "3. Copied text is not equal to expected");

            var count = textEditBox.Text.Length;
            textEditBox.ClickAt(clickStr, clickChr);
            for (var i = 0; i < count; i++)
            {
                var text = textEditBox.Text;
                var deletedCount = Math.Min(text.Length, text.StartsWith("\r\n") ? 2 : 1);
                textEditBox.PressDeleteKey();
                Assert.AreEqual(text.Substring(deletedCount), textEditBox.Text,
                    "4. Text wasn't delete");
            }

            textEditBox.RightClick();
            contextMenu.Paste();
            Assert.AreEqual(ClipboardHelper.GetText(textEditBox.Text), textEditBox.Text,
                "5. Copied text wasn't paste");

            for (var i = 0; i < count; i++)
            {
                var text = textEditBox.Text;
                var deletedCount = Math.Min(text.Length, text.EndsWith("\r\n") ? 2 : 1);
                textEditBox.PressBackspaceKey();
                Assert.AreEqual(text.Substring(0, text.Length - deletedCount), textEditBox.Text,
                    "6. Text wasn't delete");
            }

            SaveFileAs(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "7. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "8. Current opened file doesn't displayed");
        }

        [Test]
        public void SystemTest9()
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("small.txt");
            var fileToSave = EnvironmentHelper.GetOutputPath("system_test_9.txt");
            var (clickStr, clickChr) = (0, 0);

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            SaveFileAs(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "3. File wasn't save");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "4. Current opened file doesn't displayed");

            OpenFile(fileToSave);
            Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                "5. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToSave,
                "6. Current opened file doesn't displayed");

            var count = textEditBox.Text.Length;
            for (var i = 0; i < 20; i++)
            {
                textEditBox.ClickAt(clickStr, clickChr);
                var toEnter = TextHelper.GenerateRandom(2);
                var text = textEditBox.Text;
                var deletedCount = Math.Min(text.Length, text.StartsWith("\r\n") ? 2 : 1);
                textEditBox.PressDeleteKey();
                Assert.AreEqual(text.Substring(deletedCount), textEditBox.Text,
                    $"{3 * i + 7}. Text wasn't delete");
                textEditBox.EnterOneLineText(toEnter);
                Assert.AreEqual(toEnter + text.Substring(deletedCount), textEditBox.Text,
                    $"{3 * i + 8}. Text wasn't enter");
                SaveFile();
                Assert.AreEqual(File.ReadAllText(fileToSave), textEditBox.Text,
                    $"{3 * i + 9}. File wasn't save");
            }
        }

        [Test]
        public void SystemTest10()
        {
            var fileToOpen = EnvironmentHelper.GetResourcePath("small.txt");
            var (selectStartStr, selectStartChr, selectEndStr, selectEndChr) = (0, 5, 2, 10);

            var textEditBox = MainWindow.TextEditBoxForm;
            OpenFile(fileToOpen);
            Assert.AreEqual(File.ReadAllText(fileToOpen), textEditBox.Text,
                "1. File wasn't open");
            Assert.AreEqual(MainWindow.CurrentOpenedFile, fileToOpen,
                "2. Current opened file doesn't displayed");

            var textBeforeCopy = textEditBox.Text;
            textEditBox.Select(selectStartStr, selectStartChr, selectEndStr, selectEndChr);
            textEditBox.RightClick();
            var contextMenu = MainWindow.ContextMenuForm;
            contextMenu.Copy();
            var copiedText = TextHelper.GetTextInBounds(textEditBox.SplittedText,
                selectStartStr, selectStartChr,
                selectEndStr, selectEndChr);
            Assert.AreEqual(copiedText, ClipboardHelper.GetText(copiedText),
                "3. Copied text is not equal to expected");
            Assert.AreEqual(textBeforeCopy, textEditBox.Text,
                "4. Text after copy is not equal to test before copy");

            var remainingText = TextHelper.GetTextNotInBounds(textEditBox.SplittedText,
                selectStartStr, selectStartChr,
                selectEndStr, selectEndChr);
            textEditBox.PressBackspaceKey();
            Assert.AreEqual(remainingText, textEditBox.Text,
                "5. Text wasn't delete");

            textEditBox.RightClick();
            contextMenu.Paste();
            Assert.AreEqual(copiedText, ClipboardHelper.GetText(copiedText),
                "6. Copied text is not equal to expected");
            Assert.AreEqual(textBeforeCopy, textEditBox.Text,
                "7. Text after paste is not equal to test before copy");

            textEditBox.Select(selectStartStr, selectStartChr, selectEndStr, selectEndChr);
            textEditBox.PressDeleteKey();
            Assert.AreEqual(remainingText, textEditBox.Text,
                "8. Text wasn't delete");
        }
    }
}