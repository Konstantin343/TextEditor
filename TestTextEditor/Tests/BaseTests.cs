using System.Collections.Generic;
using System.IO;
using System.Threading;
using NUnit.Framework;
using TestTextEditor.Framework;
using TestTextEditor.Framework.Forms;
using TestTextEditor.Framework.Forms.TextForms;
using TestTextEditor.Framework.Windows;

namespace TestTextEditor.Tests
{
    public abstract class BaseTests
    {
        protected MainTextEditorWindow MainWindow;
        private readonly IList<string> _createdFiles = new List<string>();

        [SetUp]
        public void StartApp()
        {
            TextEditorAppLoader.StartApp();
            MainWindow = new MainTextEditorWindow(TextEditorAppLoader.Window);
        }

        [TearDown]
        public void CloseApp()
        {
            TextEditorAppLoader.CloseApp();
            foreach (var file in _createdFiles)
            {
                File.Delete(file);
            }

            _createdFiles.Clear();
        }

        protected static void EnterAndClick(
            TextEditBoxForm textEditBox,
            IList<string> textToInsert,
            int strToClick,
            int chrToClick)
        {
            textEditBox.EnterMultiLineText(textToInsert);
            textEditBox.ClickAt(strToClick, chrToClick);
        }

        protected static void EnterAndSelect(
            TextEditBoxForm textEditBox,
            IList<string> textToInsert,
            int startStr, int startChr,
            int endStr, int endChr)
        {
            textEditBox.EnterMultiLineText(textToInsert);
            textEditBox.Select(startStr, startChr, endStr, endChr);
        }

        protected void OpenFile(string filePath)
        {
            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.OpenFile();
            var openFileModalForm = MainWindow.OpenFileModalForm;
            openFileModalForm.EnterText(filePath);
            openFileModalForm.Submit();
        }

        protected void SaveFileAs(string filePath)
        {
            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.SaveAsFile();
            var saveFileAsModalForm = MainWindow.SaveFileAsModalForm;
            saveFileAsModalForm.EnterText(filePath);
            saveFileAsModalForm.Submit();
            _createdFiles.Add(filePath);
            while (!File.Exists(filePath)) Thread.Sleep(10);
        }

        protected void SaveFile()
        {
            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.SaveFile();
        }

        protected void NewFile()
        {
            var fileMenu = MainWindow.FileMenuForm;
            fileMenu.Click();
            fileMenu.NewFile();
        }
    }
}