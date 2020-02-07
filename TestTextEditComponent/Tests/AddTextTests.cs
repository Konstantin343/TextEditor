using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Tests
{
    [TestFixture]
    public class AddTextTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.AddOneLineProvider))]
        public void AddLineTest(
            string textLine)
        {
            TestTextEditBoxModel.AddLine(textLine);
            Assert.AreEqual(textLine, TestTextEditBoxModel.FirstLine,
                "Line isn't equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.AddTextProvider))]
        public void AddTextTest(
            IList<string> textLines)
        {
            TestTextEditBoxModel.AddLines(textLines);
            Assert.AreEqual(textLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.AddTextProvider))]
        public void PositionAfterAddTests(
            IList<string> textLines)
        {
            TestTextEditBoxModel.AddLines(textLines);
            Assert.AreEqual(
                new TextPosition(
                    TestTextEditBoxModel.TextLines.Count - 1,
                    TestTextEditBoxModel.TextLines.Last().Length),
                TestTextEditBoxModel.CurrentPosition,
                "Position not in the end");
        }
        
        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.AddTextOnPositionProvider))]
        public void AddTextOnPositionTests(
            IList<string> textLines,
            IList<string> expectedLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.AddLines(textLines);
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        }
        
        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.TabulationOnPositionProvider))]
        public void AddTabulationOnPositionTests(
            IList<string> textLines,
            IList<string> expectedLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.Tabulation();
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        }
        
        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.InsertTextProvider))]
        public void InsertTextTests(
            IList<string> textLines,
            IList<string> expectedLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.ChangeInsertMode();
            TestTextEditBoxModel.AddLines(textLines);
            Assert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        }
    }
}