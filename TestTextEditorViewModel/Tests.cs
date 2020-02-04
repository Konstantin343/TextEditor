﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using NUnit.Framework;
using TextEditor;
using TextEditor.ViewModel;

namespace TestTextEditorViewModel
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var textEditorViewModel = new TextEditorViewModel();
            textEditorViewModel.OpenFile("D:\\1.txt");
            textEditorViewModel.RawTextLines.Add("333");
            textEditorViewModel.SaveFile();
            textEditorViewModel.ThemesService.SelectTheme("Gold");
            Assert.AreEqual(textEditorViewModel.ThemesService.CurrentTheme.Name, "Golds");
        }
    }
}