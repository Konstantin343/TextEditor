using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestTextEditComponent.TestData;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.DataProviders
{
    public class PositionProviders
    {
        public static IEnumerable PositionProvider =>
            BaseTestsObjects.Positions.Select(position => new TestCaseData(BaseTestsObjects.TextLines, position)
                .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}"));

        public static IEnumerable UpProvider =>
            (from position in BaseTestsObjects.Positions
                let newStr = Math.Max(0, position.Str - 1)
                let newChr = Math.Min(BaseTestsObjects.TextLines[newStr].Length, position.Chr)
                let expected = new TextPosition(newStr, newChr)
                select new TestCaseData(BaseTestsObjects.TextLines, position, expected)
                    .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}")).Cast<object>();

        public static IEnumerable DownProvider =>
            (from position in BaseTestsObjects.Positions
                let newStr = Math.Min(BaseTestsObjects.TextLines.Count - 1, position.Str + 1)
                let newChr = Math.Min(BaseTestsObjects.TextLines[newStr].Length, position.Chr)
                let expected = new TextPosition(newStr, newChr)
                select new TestCaseData(BaseTestsObjects.TextLines, position, expected)
                    .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}")).Cast<object>();

        public static IEnumerable LeftProvider
        {
            get
            {
                foreach (var position in BaseTestsObjects.Positions)
                {
                    var newPosition = new TextPosition(position);
                    if (position.Str != 0 || position.Chr != 0)
                    {
                        newPosition = position.Chr == 0
                            ? new TextPosition(position.Str - 1, BaseTestsObjects.TextLines[position.Str - 1].Length)
                            : new TextPosition(position.Str, position.Chr - 1);
                    }

                    yield return new TestCaseData(BaseTestsObjects.TextLines, position, newPosition)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }

        public static IEnumerable RightProvider
        {
            get
            {
                foreach (var position in BaseTestsObjects.Positions)
                {
                    var newPosition = new TextPosition(position);
                    if (position.Str != BaseTestsObjects.TextLines.Count - 1 ||
                        position.Chr != BaseTestsObjects.TextLines.Last().Length)
                    {
                        newPosition = position.Chr == BaseTestsObjects.TextLines[position.Str].Length
                            ? new TextPosition(position.Str + 1, 0)
                            : new TextPosition(position.Str, position.Chr + 1);
                    }

                    yield return new TestCaseData(BaseTestsObjects.TextLines, position, newPosition)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }
    }
}