using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditComponent.TestData;
using TestTools.Utils;

namespace TestTextEditComponent.DataProviders
{
    public class AddTextProviders
    {
        public static IEnumerable AddOneLineProvider
        {
            get
            {
                foreach (var (textLine, testName) in BaseTestsObjects.Lines)
                {
                    yield return new TestCaseData(textLine)
                        .SetName(testName + "_{m}");
                }
            }
        }

        public static IEnumerable AddTextProvider
        {
            get
            {
                foreach (var (text, testName) in BaseTestsObjects.Texts)
                {
                    yield return new TestCaseData(new List<string>(text))
                        .SetName(testName + "_{m}");
                }
            }
        }

        public static IEnumerable AddTextOnPositionProvider
        {
            get
            {
                var textLines = BaseTestsObjects.TextLines;
                var positions = BaseTestsObjects.Positions;

                foreach (var position in positions)
                {
                    var expectedText = TextHelper.SplittedInsertLinesInText(
                        textLines, textLines, position.Str, position.Chr);
                    yield return new TestCaseData(textLines, expectedText, position)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }

        public static IEnumerable TabulationOnPositionProvider
        {
            get
            {
                var textLines = BaseTestsObjects.TextLines;
                var positions = BaseTestsObjects.Positions;

                foreach (var position in positions)
                {
                    var expectedText = TextHelper.SplittedInsertLinesInText(
                        textLines, new[] {"\t"}, position.Str, position.Chr);
                    yield return new TestCaseData(textLines, expectedText, position)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }

        public static IEnumerable InsertTextProvider
        {
            get
            {
                var textLines = BaseTestsObjects.TextLines;
                var positions = BaseTestsObjects.Positions;

                foreach (var position in positions)
                {
                    var expectedLines = new List<string>(textLines);
                    expectedLines[position.Str] = expectedLines[position.Str].Substring(0, position.Chr);
                    var expectedText = TextHelper.SplittedInsertLinesInText(
                        expectedLines, textLines, position.Str, position.Chr);
                    yield return new TestCaseData(textLines, expectedText, position)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }
    }
}