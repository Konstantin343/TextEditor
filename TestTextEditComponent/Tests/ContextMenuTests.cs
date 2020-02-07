using NUnit.Framework;
using TextEditComponent.TextEditComponent;

namespace TestTextEditComponent.Tests
{
    public class ContextMenuTests : BaseTests
    {
        [Test]
        public void Test()
        {
            var textEditContextMenuModel = new TextEditContextMenuModel(new TextEditBoxModel());
            textEditContextMenuModel.SelectAllCommand.Execute(null);
            textEditContextMenuModel.CopyCommand.Execute(null);
            textEditContextMenuModel.CutCommand.Execute(null);
            textEditContextMenuModel.PasteCommand.Execute(null);
        }
    }
}