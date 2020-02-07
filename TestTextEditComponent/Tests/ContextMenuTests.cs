using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TestTextEditComponent.Models;
using TestTools.Utils;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Tests
{
    [TestFixture]
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
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.CopyCutProviders))]
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
        
        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.CopyCutProviders))]
        public void CutTest(
            IList<string> textLines,
            string expectedText,
            SelectedTextBounds bounds)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SelectText(bounds);
            ContextMenuModel.Cut();
            Assert.AreEqual(expectedText, ClipboardHelper.GetText(expectedText),
                "Text wasn't copy");
        }
        
        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.CutRemainingProviders))]
        public void CutRemainingTest(
            IList<string> textLines,
            IList<string> remainingLines,
            SelectedTextBounds bounds)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SelectText(bounds);
            ContextMenuModel.Cut();
            Assert.AreEqual(remainingLines, TestTextEditBoxModel.TextLines,
                "Text wasn't cut");
        }
        
        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.PasteProviders))]
        public void PasteOnPositionTests(
            IList<string> textLines,
            string toPaste,
            IList<string> expectedLines,
            TextPosition position)
        {
            ClipboardHelper.SetText(toPaste);
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            ContextMenuModel.Paste();
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        }
        
        [Test]
        [TestCaseSource(typeof(ContextMenuProviders), nameof(ContextMenuProviders.PasteSelectedProviders))]
        public void PasteSelectedTests(
            IList<string> textLines,
            string toPaste,
            IList<string> expectedLines,
            SelectedTextBounds bounds)
        {
            ClipboardHelper.SetText(toPaste);
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SelectText(bounds);
            ContextMenuModel.Paste();
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        }
    }
}