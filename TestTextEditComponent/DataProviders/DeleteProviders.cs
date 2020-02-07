using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestTextEditComponent.TestData;
using TestTools.Utils;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.DataProviders
{
    public class DeleteProviders
    {
        public static IEnumerable DeleteBeforeProvider
        {
            get
            {
                var textLines = BaseTestsObjects.TextLines;
                var positions = BaseTestsObjects.Positions;

                foreach (var position in positions)
                {
                    var newPosition = new TextPosition(position);
                    if (position.Str != 0 || position.Chr != 0)
                    {
                        newPosition = position.Chr == 0
                            ? new TextPosition(position.Str - 1, textLines[position.Str - 1].Length)
                            : new TextPosition(position.Str, position.Chr - 1);
                    }

                    var expectedText = TextHelper.GetSplittedTextNotInBounds(
                        textLines, newPosition.Str, newPosition.Chr, position.Str, position.Chr);
                    yield return new TestCaseData(textLines, expectedText, position)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }

        public static IEnumerable DeleteAfterProvider
        {
            get
            {
                var textLines = BaseTestsObjects.TextLines;
                var positions = BaseTestsObjects.Positions;

                foreach (var position in positions)
                {
                    var newPosition = new TextPosition(position);
                    if (position.Str != textLines.Count - 1 || position.Chr != textLines.Last().Length)
                    {
                        newPosition = position.Chr == textLines[position.Str].Length
                            ? new TextPosition(position.Str + 1, 0)
                            : new TextPosition(position.Str, position.Chr + 1);
                    }

                    var expectedText = TextHelper.GetSplittedTextNotInBounds(
                        textLines, position.Str, position.Chr, newPosition.Str, newPosition.Chr);
                    yield return new TestCaseData(textLines, expectedText, position)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }
    }
}