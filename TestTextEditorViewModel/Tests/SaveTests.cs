using System.IO;
using NUnit.Framework;
using TestTextEditorViewModel.DataProviders;
using TestTools.Utils;

namespace TestTextEditorViewModel.Tests
{
    [TestFixture]
    public class SaveTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(FileProviders), nameof(FileProviders.ResourceFilesProvider))]
        public void SaveAfterOpenTest(
            string fileName)
        {
            TestViewModel.OpenFile(fileName);
            TestViewModel.SetText(TextHelper.GetText());
            TestViewModel.SaveFile();
            Assert.AreEqual(File.ReadAllText(fileName), TestViewModel.Text,
                "Text is wrong");
        }
        
        [Test]
        [TestCaseSource(typeof(FileProviders), nameof(FileProviders.FilesProvider))]
        public void SaveAfterSaveAsTest(
            string fileName)
        {
            TestViewModel.SetText(TextHelper.GetText(20, 20));
            TestViewModel.SaveAsFile(fileName);
            TestViewModel.SetText(TextHelper.GetText(10, 40));
            TestViewModel.SaveFile();
            Assert.AreEqual(File.ReadAllText(fileName), TestViewModel.Text,
                "Text is wrong");
        }
    }
}