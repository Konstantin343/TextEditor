using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Tests
{
    public class PositionTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(PositionProviders), nameof(PositionProviders.PositionProvider))]
        public void PositionTest(
            IList<string> textLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            Assert.AreEqual(position, TestTextEditBoxModel.CurrentPosition,
                "Position wasn't set");
        }

        [Test]
        [TestCaseSource(typeof(PositionProviders), nameof(PositionProviders.UpProvider))]
        public void UpTest(
            IList<string> textLines,
            TextPosition position,
            TextPosition expected)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.OneLineUp();
            Assert.AreEqual(expected, TestTextEditBoxModel.CurrentPosition,
                "Position wasn't change");
        }

        [Test]
        [TestCaseSource(typeof(PositionProviders), nameof(PositionProviders.DownProvider))]
        public void DownTest(
            IList<string> textLines,
            TextPosition position,
            TextPosition expected)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.OneLineDown();
            Assert.AreEqual(expected, TestTextEditBoxModel.CurrentPosition,
                "Position wasn't change");
        }

        [Test]
        [TestCaseSource(typeof(PositionProviders), nameof(PositionProviders.LeftProvider))]
        public void LeftTest(
            IList<string> textLines,
            TextPosition position,
            TextPosition expected)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.OneCharLeft();
            Assert.AreEqual(expected, TestTextEditBoxModel.CurrentPosition,
                "Position wasn't change");
        }

        [Test]
        [TestCaseSource(typeof(PositionProviders), nameof(PositionProviders.RightProvider))]
        public void RightTest(
            IList<string> textLines,
            TextPosition position,
            TextPosition expected)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.OneCharRight();
            Assert.AreEqual(expected, TestTextEditBoxModel.CurrentPosition,
                "Position wasn't change");
        }
    }
}