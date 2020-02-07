using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Tests
{
    public class DeleteTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(DeleteProviders), nameof(DeleteProviders.DeleteBeforeProvider))]
        public void DeleteBeforeTest(
            IList<string> textLines,
            IList<string> expectedLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.DeleteBefore();
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        } 
        
        [Test]
        [TestCaseSource(typeof(DeleteProviders), nameof(DeleteProviders.DeleteAfterProvider))]
        public void DeleteAfterTest(
            IList<string> textLines,
            IList<string> expectedLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.DeleteAfter();
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        } 
    }
}