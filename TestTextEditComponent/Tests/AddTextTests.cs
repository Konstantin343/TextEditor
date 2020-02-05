using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TextEditComponent.TextEditComponent;

namespace TestTextEditComponent.Tests
{
    [TestFixture]
    public class AddTextTests
    {
        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.AddOneLineProvider))]
        public void AddLineTest(
            string textLine)
        {
            var textBoxModel = new TextEditBoxModel();
            textBoxModel.AddTextOnCurrentPosition(textLine);
            Assert.AreEqual(textLine, textBoxModel.Text);
        }
        
        [Test]
        [TestCaseSource(typeof(AddTextProviders), nameof(AddTextProviders.AddTextProvider))]
        public void AddTextTest(
            IList<string> textLines)
        {
            var textBoxModel = new TextEditBoxModel();
            textBoxModel.AddLinesOnCurrentPosition(textLines);
            Assert.AreEqual(textLines, textBoxModel.TextLines.Lines);
        }
    }
}