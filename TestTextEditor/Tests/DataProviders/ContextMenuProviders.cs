using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NUnit.Framework;
using TestTextEditor.Tests.TestData;
using TestTools.Utils;

namespace TestTextEditor.Tests.DataProviders
{
    public class ContextMenuProviders
    {
        public static IEnumerable ContextMenuLocationProviders =>
            new[]
            {
                new Point(0, 0),
                new Point(5, 5),
                new Point(40, 25),
                new Point(30, 77),
                new Point(100, 100),
            }.Select((point, i) => new TestCaseData(point).SetName($"TestCase{i + 1}" + "_{m}"));

        public static IEnumerable SelectAllProviders
        {
            get
            {
                var testData = BaseTestObjects.BaseMultilineTexts;

                foreach (var (textToInsert, name) in testData)
                {
                    yield return new TestCaseData(textToInsert)
                        .SetName(name + "_{m}");
                }
            }
        }

        public static IEnumerable CopyProviders
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
            }
        }

        public static IEnumerable CutProviders
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
                                strTo, chrTo),
                            TextHelper.GetTextNotInBounds(
                                textToInsert,
                                strFrom, chrFrom,
                                strTo, chrTo))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }

        public static IEnumerable PasteProviders
        {
            get
            {
                var testData = new[]
                {
                    (new List<string>(new[] {"texttexttexttext", "texttexttextetext", "texttextext"}), 0, 1),
                    (new List<string>(new[] {"", "", "", ""}), 1, 2),
                    (TextHelper.GetText(100, 3), 2, 1),
                    (TextHelper.GetText(5, 200), 3, 2),
                    (new List<string>(new[] {"aaaaaaaa", "bbbbb", "", "dddddd", "", "ffffffff"}), 0, 0),
                    (new List<string>(new[] {"aaaaaaaa", "bbbbb", ""}), 7, 14),
                    (new List<string>(new[] {"", "bbbbb", "cccccccccc"}), 7, 5),
                    (new List<string>(new[] {"         ", "   ", "            ", "    "}), 4, 5),
                    (new List<string>(new[] {"aaaaa", "   ", "   ", "dddd"}), 2, 3),
                    (new List<string>(new[] {"         ", "   ", "aaaa", "scascsaca"}), 5, 0),
                    (new List<string>(new[] {"dasdadas", "esesese", "          ", "    "}), 0, 10),
                };

                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;

                for (var j = 0; j < testData.Length; j++)
                {
                    var (linesToPaste, strToPaste, chrToPaste) = testData[j];

                    yield return new TestCaseData(
                            textToInsert,
                            string.Join("\r\n", linesToPaste),
                            strToPaste, chrToPaste, 
                            TextHelper.InsertLinesInText(
                                textToInsert,
                                linesToPaste,
                                strToPaste, chrToPaste))
                        .SetName($"TestCase{j + 1}" + "_{m}");
                }
            }
        }
    }
}