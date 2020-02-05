using System.Collections.Generic;
using NUnit.Framework;
using TextEditComponent.TextEditComponent;

namespace TestTextEditComponent.Tests
{
    [TestFixture]
    public class AddTextTests
    {
        [Test]
        public void AddLineTest(
            string textLine,
            IList<string> expectedLine)
        {
            var textBoxModel = new TextEditBoxModel();
            textBoxModel.AddTextOnCurrentPosition(textLine);
            Assert.AreEqual(expectedLine, textBoxModel.Text);
        }
        
        [Test]
        public void AddTextTest(
            IList<string> textLines,
            IList<string> expectedLines)
        {
            var textBoxModel = new TextEditBoxModel();
            textBoxModel.AddLinesOnCurrentPosition(textLines);
            Assert.AreEqual(expectedLines, textBoxModel.TextLines.Lines);
        }
    }
}