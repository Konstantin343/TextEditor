using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Tests
{
    public class SelectedTextTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(SelectTextProviders), nameof(SelectTextProviders.SelectedBoundsProvider))]
        public void SelectedTextBoundsTest(
            IList<string> textLines,
            SelectedTextBounds bounds)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SelectText(bounds);
            Assert.AreEqual(bounds, TestTextEditBoxModel.SelectedTextBounds,
                "Bounds isn't equal to expected");
        }
        
        [Test]
        [TestCaseSource(typeof(SelectTextProviders), nameof(SelectTextProviders.DeleteSelectedTextProvider))]
        public void DeleteSelectedTextTest(
            IList<string> textLines,
            IList<string> expectedLines,
            SelectedTextBounds bounds)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SelectText(bounds);
            TestTextEditBoxModel.DeleteSelected();
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Remaining text isn't equal to expected");
        }
    }
}