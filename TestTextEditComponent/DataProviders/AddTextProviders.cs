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
                var textLines = new[]
                {
                    ("abcdefghijklmnopqrstuvwxyz", "LatinAlphabet"),
                    ("~`!@\"#№$;%^:&?*()-_=+|\\/,.'<>", "SpecSymbols"),
                    ("\u1111\u2222\u3333\u1234\u2342", "UnicodeSymbols"),
                    ("абвгдеёжзийклмнопрстуфхцчшщъьэюя", "CyrillicSymbols"),
                    ("0123456789", "Numbers"),
                    ("       ", "OnlySpaces"),
                    ("", "Empty"),
                    ("text text", "SpaceMiddle"),
                    (" text", "SpaceBegin"),
                    ("text ", "SpaceEnd"),
                    (" text ", "SpaceBeginEnd"),
                    (" text text ", "SpaceBeginMiddleEnd"),
                    ("QWERTY", "UpperCase"),
                    (TextHelper.GenerateRandom(1000), "LongString"),
                    (TextHelper.GenerateRandom(100000), "VeryLongString"),
                    ("  QWE ewqeqw eqwe 123 \u1235 $%@ 12 #@#  textetxttext ", "DifferentSymbols")
                };
                foreach (var (textLine, testName) in textLines)
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
                var texts = new[]
                {
                    (new[] {"abcdefghi", "jklmnopqrst", "uvwxyz"}, "LatinAlphabet"),
                    (new[] {"", "abcdefghi", "jklmnopqrst", "uvwxyz"}, "EmptyLineBegin"),
                    (new[] {"abcdefghi", "jklmnopqrst", "uvwxyz", ""}, "EmptyLineEnd"),
                    (new[] {"abcdefghi", "jklmnopqrst", "", "uvwxyz"}, "EmptyLineMiddle"),
                    (new[] {"", "abcdefghi", "jklmnopqrst", "uvwxyz", ""}, "EmptyLineBeginEnd"),
                    (new[] {"", "abcdefghi", "jklmnopqrst", "", "uvwxyz", ""}, "EmptyLineEnd"),
                    (new[] {"    ", "abcdefghi", "jklmnopqrst", "uvwxyz"}, "SpaceLineBegin"),
                    (new[] {"abcdefghi", "jklmnopqrst", "uvwxyz", "     "}, "SpaceLineEnd"),
                    (new[] {"abcdefghi", "jklmnopqrst", "     ", "uvwxyz"}, "SpaceLineMiddle"),
                    (new[] {"     ", "abcdefghi", "jklmnopqrst", "uvwxyz", "     "}, "SpaceLineBeginEnd"),
                    (new[] {"     ", "abcdefghi", "jklmnopqrst", "    ", "uvwxyz", "     "}, "SpaceLineEnd"),
                    (new[] {"  ", "", "abcdefghi", "jklmn", "", "opqrst", "   ", "uvw   xyz", "", "  "},
                        "DifferentLines"),
                    (TextHelper.GetText(1000, 1000), "LongTextLongLines"),
                    (TextHelper.GetText(100, 10000), "LongTextVeryLongLines"),
                    (TextHelper.GetText(10000, 100), "VeryLongTextLongLines"),
                };
                foreach (var (text, testName) in texts)
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