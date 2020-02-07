using NUnit.Framework;
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
            //
            textEditorViewModel.NewFile();
            textEditorViewModel.OpenFile("");
            textEditorViewModel.SaveFile();
            textEditorViewModel.SaveAsFile("");
            textEditorViewModel.SelectThemeCommand.Execute("Gold");
            //
        }
    }
}