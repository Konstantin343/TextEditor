using System.Collections.Generic;
using TestTextEditor.Framework.Utils;

namespace TestTextEditor.Tests.TestData
{
    public static class BaseTestObjects
    {
        public static readonly string Marker = "$";

        public static readonly IList<string> TextToInsertSelectedTests = new List<string>(new[]
        {
            "testtext    ssss",
            "abcdefghijl",
            "fghisssssjk",
            "     ",
            "lmnwe",
            "",
            "wwwwwwwopqr",
            "stuvwsdsadsxyz"
        });

        public static readonly (int, int, int, int)[] BoundsSelectedTests =
        {
            (0, 0, 7, 14),
            (0, 3, 0, 11),
            (1, 5, 6, 7),
            (3, 0, 3, 4),
            (5, 0, 6, 0),
            (0, 4, 7, 12),
            (6, 5, 7, 6)
        };

        public static readonly (IList<string>, string)[] BaseMultilineTexts =
        {
            (new List<string>(new[] {"texttexttexttext", "texttexttextetext", "texttextext"}), "LatinText"),
            (new List<string>(new[] {"", "", "", ""}), "EmptyLines"),
            (TextHelper.GetText(100, 3), "Text100Lines"),
            (TextHelper.GetText(5, 200), "TextLongLines"),
            (new List<string>(new[] {"aaaaaaaa", "bbbbb", "", "dddddd", "", "ffffffff"}), "TextWithEmptyLines"),
            (new List<string>(new[] {"aaaaaaaa", "bbbbb", ""}), "LastLineIsEmpty"),
            (new List<string>(new[] {"", "bbbbb", "cccccccccc"}), "FirstLineIsEmpty"),
            (new List<string>(new[] {"         ", "   ", "            ", "    "}), "AllLinesOnlySpaces"),
            (new List<string>(new[] {"aaaaa", "   ", "   ", "dddd"}), "MiddleLinesOnlySpaces"),
            (new List<string>(new[] {"         ", "   ", "aaaa", "scascsaca"}), "FirstLinesOnlySpaces"),
            (new List<string>(new[] {"dasdadas", "esesese", "          ", "    "}), "LastLinesOnlySpaces"),
        };

        public static readonly IList<string> BaseThemes = 
            new List<string>(new[] {"Classic", "Gold", "White", "Black"});
    }
}