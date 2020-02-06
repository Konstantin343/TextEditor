using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.DataProviders
{
    public class NewLinesProviders
    {
        public static IEnumerable NewLineProvider
        {
            get
            {
                var textLines = new List<string>(new[]
                    {"     ", "", "abcdefghi", "jklmn", "", "opqrst", "      ", "uvw   xyz", "", "     "});

                var positions = new[]
                {
                    new TextPosition(0, 0),
                    new TextPosition(1, 0),
                    new TextPosition(2, 0),
                    new TextPosition(3, 0),
                    new TextPosition(4, 0),
                    new TextPosition(5, 0),
                    new TextPosition(6, 0),
                    new TextPosition(7, 0),
                    new TextPosition(8, 0),
                    new TextPosition(9, 0),
                    new TextPosition(0, 2),
                    new TextPosition(2, 3),
                    new TextPosition(9, 2),
                    new TextPosition(7, 4),
                    new TextPosition(9, 5),
                    new TextPosition(5, 2),
                };

                foreach (var position in positions)
                {
                    var expectedText = new List<string>(textLines);
                    var toInsert = position.Chr < expectedText[position.Str].Length
                        ? expectedText[position.Str].Substring(position.Chr)
                        : string.Empty;
                    expectedText.Insert(position.Str + 1, toInsert);
                    if (position.Chr < expectedText[position.Str].Length)
                        expectedText[position.Str] = expectedText[position.Str].Remove(position.Chr);
                    yield return new TestCaseData(textLines, expectedText, position)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }
    }
}