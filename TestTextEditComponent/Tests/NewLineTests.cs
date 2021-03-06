﻿using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.DataProviders;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.Tests
{
    [TestFixture]
    public class NewLineTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(NewLinesProviders), nameof(NewLinesProviders.NewLineProvider))]
        public void NewLineTest(
            IList<string> textLines,
            IList<string> expectedLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.NewLine();
            CollectionAssert.AreEqual(expectedLines, TestTextEditBoxModel.TextLines,
                "Lines isn't equal to expected");
        }

        [Test]
        [TestCaseSource(typeof(NewLinesProviders), nameof(NewLinesProviders.NewLineProvider))]
        public void PositionAfterNewLineTest(
            IList<string> textLines,
            IList<string> expectedLines,
            TextPosition position)
        {
            TestTextEditBoxModel.AddLines(textLines);
            TestTextEditBoxModel.SetPositionTo(position);
            TestTextEditBoxModel.NewLine();
            Assert.AreEqual(
                new TextPosition(position.Str + 1, 0),
                TestTextEditBoxModel.CurrentPosition,
                "Position not in the end");
        }
    }
}