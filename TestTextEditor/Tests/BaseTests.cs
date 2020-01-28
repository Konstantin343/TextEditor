﻿using System.Collections.Generic;
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

        [SetUp]
        public void StartApp()
        {
            TextEditorAppLoader.StartApp();
            MainWindow = new MainTextEditorWindow(TextEditorAppLoader.Window);
        }

        [TearDown]
        public void CloseApp() => TextEditorAppLoader.CloseApp();
        
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
    }
}