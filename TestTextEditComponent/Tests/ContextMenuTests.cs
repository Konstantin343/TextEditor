using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TestTextEditComponent.Models;
using TestTools.Utils;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Tests
{
    public class ContextMenuTests : BaseTests
    {
        protected TestContextMenuModel ContextMenuModel;

        [SetUp]
        public void OwnSetUp() =>
            ContextMenuModel = new TestContextMenuModel(TestTextEditBoxModel);

        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.SelectAllProviders))]
        public void SelectAllTest(
            IList<string> textLines,
            SelectedTextBounds bounds)
        {
            TestTextEditBoxModel.AddLines(textLines);
            ContextMenuModel.SelectAll();
            Assert.AreEqual(bounds, TestTextEditBoxModel.SelectedTextBounds,
                "Bounds isn't equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.CopyProviders))]
        public void CopyTest(
            IList<string> textLines,
            string expectedText,
            SelectedTextBounds bounds)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SelectText(bounds);
            ContextMenuModel.Copy();
            Assert.AreEqual(expectedText, ClipboardHelper.GetText(expectedText),
                "Text wasn't copy");
        }
    }
}