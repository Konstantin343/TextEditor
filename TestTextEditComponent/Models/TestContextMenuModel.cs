using System;
using System.Threading;
using TestTools.Logger;
using TextEditComponent.TextEditComponent;

namespace TestTextEditComponent.Models
{
    public class TestContextMenuModel
    {
        public TextEditContextMenuModel ContextMenuModel { get; }

        public TestContextMenuModel(TestTextEditBoxModel testTextEditBoxModel) =>
            ContextMenuModel = new TextEditContextMenuModel(testTextEditBoxModel.TextEditBoxModel);

        public void Copy()
        {
            TestLogger.Instance.Info("Copy text to clipboard");
            Execute(() => ContextMenuModel.CopyCommand.Execute(null));
        }

        public void Cut()
        {
            TestLogger.Instance.Info("Cut text to clipboard");
            Execute(() => ContextMenuModel.CutCommand.Execute(null));
        }

        public void Paste()
        {
            TestLogger.Instance.Info("Paste text in text edit box");
            Execute(() => ContextMenuModel.PasteCommand.Execute(null));
        }

        public void SelectAll()
        {
            TestLogger.Instance.Info("Select all text in text edit box");
            Execute(() => ContextMenuModel.SelectAllCommand.Execute(null));
        }

        private void Execute(Action action)
        {
            var thread = new Thread(() => action());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}