using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TestStack.White.WindowsAPI;
using TestTextEditor.Tests.TestData;
using TestTools.Utils;

namespace TestTextEditor.Tests.DataProviders
{
    public class KeysProviders
    {
        public static IEnumerable BackspaceInLineProviders
        {
            get
            {
                var testData = new[]
                {
                    (1, 2),
                    (2, 4),
                    (0, 1),
                    (3, 8),
                    (2, 6),
                    (3, 1)
                };

                var textToInsert = new List<string>(new[]
                {
                    "abcde",
                    "fgh ijk",
                    "lmno pqr",
                    "stu vwxyz"
                });

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) => i == str ? s.Remove(chr - 1, 1) : s));

                    yield return new TestCaseData(textToInsert, str, chr, expectedText)
                        .SetName($"InLine{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable BackspaceInBeginProviders
        {
            get
            {
                var textToInsert = new List<string>(new[]
                {
                    "abcde",
                    "",
                    "fghijk",
                    "     ",
                    "lmnopqr",
                    "stuvwxyz"
                });

                for (var j = 0; j < textToInsert.Count; j++)
                {
                    var expectedText = new StringBuilder();
                    for (var i = 0; i < textToInsert.Count; i++)
                    {
                        if (i != j && i != 0)
                        {
                            expectedText.Append("\r\n");
                        }

                        expectedText.Append(textToInsert[i]);
                    }

                    yield return new TestCaseData(textToInsert, j, 0, expectedText.ToString())
                        .SetName($"InBeginLine{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable DeleteInLineProviders
        {
            get
            {
                var testData = new[]
                {
                    (1, 2),
                    (2, 4),
                    (0, 1),
                    (3, 8),
                    (2, 6),
                    (3, 1)
                };

                var textToInsert = new List<string>(new[]
                {
                    "abcde",
                    "fgh ijk",
                    "lmno pqr",
                    "stu vwxyz"
                });

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) => i == str ? s.Remove(chr, 1) : s));

                    yield return new TestCaseData(textToInsert, str, chr, expectedText)
                        .SetName($"InLine{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable DeleteInEndProviders
        {
            get
            {
                var textToInsert = new List<string>(new[]
                {
                    "abcde",
                    "",
                    "fghijk",
                    "     ",
                    "lmnopqr",
                    "stuvwxyz"
                });

                for (var j = 0; j < textToInsert.Count; j++)
                {
                    var expectedText = new StringBuilder();
                    for (var i = 0; i < textToInsert.Count; i++)
                    {
                        expectedText.Append(textToInsert[i]);

                        if (i != j && i != textToInsert.Count - 1)
                        {
                            expectedText.Append("\r\n");
                        }
                    }

                    yield return new TestCaseData(textToInsert, j, textToInsert[j].Length, expectedText.ToString())
                        .SetName($"InBeginLine{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable DeleteBackspaceSelectedTextProviders
        {
            get
            {
                var testData = BaseTestObjects.BoundsSelectedTests;
                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (strFrom, chrFrom, strTo, chrTo) = testData[j];

                    yield return new TestCaseData(textToInsert, strFrom, chrFrom, strTo, chrTo,
                            TextHelper.GetTextNotInBounds(
                                textToInsert,
                                strFrom, chrFrom,
                                strTo, chrTo))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable EnterProviders
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
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n",
                        textToInsert.Select((s, i) => i == str ? s.Insert(chr, "\r\n") : s));

                    yield return new TestCaseData(textToInsert, str, chr, expectedText)
                        .SetName($"InLine{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable EnterSelectedTextProviders
        {
            get
            {
                var testData = BaseTestObjects.BoundsSelectedTests;
                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (strFrom, chrFrom, strTo, chrTo) = testData[j];

                    yield return new TestCaseData(textToInsert, strFrom, chrFrom, strTo, chrTo,
                            TextHelper.GetTextNotInBounds(
                                textToInsert,
                                strFrom, chrFrom,
                                strTo, chrTo,
                                "\r\n"))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable TabProviders
        {
            get
            {
                var testData = new[]
                {
                    (0, 10),
                    (1, 4),
                    (2, 5),
                    (3, 2),
                    (4, 5),
                    (5, 0),
                    (6, 0)
                };

                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) => i == str ? s.Insert(chr, "\t") : s));

                    yield return new TestCaseData(textToInsert, str, chr, expectedText)
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable TabSelectedTextProviders
        {
            get
            {
                var testData = BaseTestObjects.BoundsSelectedTests;
                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (strFrom, chrFrom, strTo, chrTo) = testData[j];

                    yield return new TestCaseData(textToInsert, strFrom, chrFrom, strTo, chrTo,
                            TextHelper.GetTextNotInBounds(
                                textToInsert,
                                strFrom, chrFrom,
                                strTo, chrTo,
                                "\t"))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable UpArrowProviders
        {
            get
            {
                var testData = new[]
                {
                    (0, 0),
                    (0, 5),
                    (1, 0),
                    (1, 10),
                    (2, 11),
                    (3, 3),
                    (4, 3),
                    (5, 0),
                    (6, 5),
                    (7, 14)
                };

                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) => i == Math.Max(str - 1, 0)
                            ? s.Insert(Math.Min(chr, s.Length), BaseTestObjects.Marker)
                            : s));

                    yield return new TestCaseData(textToInsert, BaseTestObjects.Marker, str, chr, expectedText,
                            KeyboardInput.SpecialKeys.UP)
                        .SetName($"Up{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable DownArrowProviders
        {
            get
            {
                var testData = new[]
                {
                    (0, 0),
                    (0, 10),
                    (0, 16),
                    (1, 11),
                    (2, 3),
                    (3, 3),
                    (4, 3),
                    (6, 11),
                    (7, 5),
                    (7, 14)
                };

                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) => i == Math.Min(str + 1, textToInsert.Count - 1)
                            ? s.Insert(Math.Min(chr, s.Length), BaseTestObjects.Marker)
                            : s));

                    yield return new TestCaseData(textToInsert, BaseTestObjects.Marker, str, chr, expectedText,
                            KeyboardInput.SpecialKeys.DOWN)
                        .SetName($"Dowm{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable LeftArrowProviders
        {
            get
            {
                var testData = new[]
                {
                    (0, 0),
                    (0, 2),
                    (0, 10),
                    (1, 0),
                    (3, 3),
                    (4, 3),
                    (7, 5),
                };

                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) =>
                        {
                            if (chr == 0 && str == 0 && i == 0)
                                return s.Insert(chr, BaseTestObjects.Marker);
                            if (chr == 0 && i == str - 1)
                                return s.Insert(s.Length, BaseTestObjects.Marker);
                            if (chr != 0 && i == str)
                                return s.Insert(chr - 1, BaseTestObjects.Marker);
                            return s;
                        }));

                    yield return new TestCaseData(textToInsert, BaseTestObjects.Marker, str, chr, expectedText,
                            KeyboardInput.SpecialKeys.LEFT)
                        .SetName($"Left{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable RightArrowProviders
        {
            get
            {
                var testData = new[]
                {
                    (0, 0),
                    (0, 4),
                    (0, 15),
                    (1, 11),
                    (3, 3),
                    (4, 3),
                    (7, 14),
                };

                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) =>
                        {
                            if (chr == s.Length && str == textToInsert.Count - 1 && i == textToInsert.Count - 1)
                                return s.Insert(chr, BaseTestObjects.Marker);
                            if (chr == s.Length && i == str + 1)
                                return s.Insert(0, BaseTestObjects.Marker);
                            if (chr != s.Length && i == str)
                                return s.Insert(chr + 1, BaseTestObjects.Marker);
                            return s;
                        }));

                    yield return new TestCaseData(textToInsert, BaseTestObjects.Marker, str, chr, expectedText,
                            KeyboardInput.SpecialKeys.RIGHT)
                        .SetName($"Right{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable InsertProviders
        {
            get
            {
                var testData = new[]
                {
                    (0, 0),
                    (0, 4),
                    (0, 15),
                    (1, 11),
                    (3, 3),
                    (4, 3),
                    (7, 14),
                };

                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (str, chr) = testData[j];
                    var expectedText = string.Join("\r\n", textToInsert.Select(
                        (s, i) =>
                        {
                            if (i != str) return s;
                            return chr == s.Length
                                ? s.Insert(chr, BaseTestObjects.Marker)
                                : s.Remove(chr, 1).Insert(chr, BaseTestObjects.Marker);
                        }));

                    yield return new TestCaseData(textToInsert, BaseTestObjects.Marker, str, chr, expectedText)
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }
    }
}