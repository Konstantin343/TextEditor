using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.TestData;

namespace TestTextEditor.Tests.DataProviders
{
    public class TextProviders
    {
        public static IEnumerable OneLineTextProviders
        {
            get
            {
                var testData = new[]
                {
                    ("abcdefghijklmnopqrstuvwxyz", "LatinAlphabet"),
                    ("0123456789", "Numbers"),
                    ("~!@#$%^&*()-_=+/,.<>{}[]|;:?\"'`", "SpecSymbols"),
                    ("", "Empty"),
                    ("      ", "OnlySpaces"),
                    ("aaaa      bbbbb", "SpacesMiddle"),
                    ("aaaaaaa      ", "SpacesEnd"),
                    ("      bbbbbbb", "SpacesBegin"),
                    ("abc130%^%$^@#23qd  d sda2 13", "AllTypes"),
                    (TextHelper.GenerateRandom(100), "Length100"),
                    (TextHelper.GenerateRandom(1000), "Length1000")
                };

                foreach (var (text, name) in testData)
                {
                    yield return new TestCaseData(text).SetName(name + "_{m}");
                }
            }
        }

        public static IEnumerable MultiLineTextProviders
        {
            get
            {
                var testData = BaseTestObjects.BaseMultilineTexts;

                foreach (var (textToInsert, name) in testData)
                {
                    var expectedText = string.Join("\r\n", textToInsert);

                    yield return new TestCaseData(textToInsert, expectedText)
                        .SetName(name + "_{m}");
                }
            }
        }

        public static IEnumerable InsertTextProviders
        {
            get
            {
                var testData = new[]
                {
                    (0, 0),
                    (0, 4),
                    (0, 10),
                    (1, 6),
                    (2, 11),
                    (4, 0),
                    (3, 5),
                    (3, 3),
                    (5, 0),
                    (7, 3)
                };

                var textToInsert = new List<string>(new[]
                {
                    "testtext      ssss",
                    "abcdefghijl",
                    "fghisssssjk",
                    "     ",
                    "lmnwe",
                    "",
                    "wwwwwwwopqr",
                    "xyz"
                });

                for (var j = 0; j < testData.Length; j++)
                {
                    var textToChange = TextHelper.GenerateRandom(5);
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n",
                        textToInsert.Select((s, i) => i == str ? s.Insert(chr, textToChange) : s));

                    yield return new TestCaseData(textToInsert, textToChange, str, chr, expectedText)
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable SelectTextProviders
        {
            get
            {
                var testData = BaseTestObjects.BoundsSelectedTests;
                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (strFrom, chrFrom, strTo, chrTo) = testData[j];

                    yield return new TestCaseData(textToInsert, strFrom, chrFrom, strTo, chrTo,
                            TextHelper.GetTextInBounds(
                                textToInsert,
                                strFrom, chrFrom,
                                strTo, chrTo))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }

                for (var j = 0; j < testData.Length; j++)
                {
                    var (strTo, chrTo, strFrom, chrFrom) = testData[j];

                    yield return new TestCaseData(textToInsert, strFrom, chrFrom, strTo, chrTo,
                            TextHelper.GetTextInBounds(
                                textToInsert,
                                strTo, chrTo,
                                strFrom, chrFrom))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable ChangeSelectedTextProviders
        {
            get
            {
                var testData = BaseTestObjects.BoundsSelectedTests;
                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;
                for (var j = 0; j < testData.Length; j++)
                {
                    var (strFrom, chrFrom, strTo, chrTo) = testData[j];
                    var textToChange = TextHelper.GenerateRandom(5);

                    yield return new TestCaseData(textToInsert, textToChange, strFrom, chrFrom, strTo, chrTo,
                            TextHelper.GetTextNotInBounds(
                                textToInsert,
                                strFrom, chrFrom,
                                strTo, chrTo,
                                textToChange))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }
    }
}