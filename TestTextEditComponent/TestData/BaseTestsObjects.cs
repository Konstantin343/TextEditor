using System.Collections.Generic;
using TestTools.Utils;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.TestData
{
    public static class BaseTestsObjects
    {
        public static (string, string)[] Lines = {
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
        
        public static (IList<string>, string)[] Texts = {
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

        public static IList<string> TextLines = new List<string>(new[]
            {"     ", "", "abcdefghi", "jklmn", "", "opqrst", "      ", "uvw   xyz", "", "     "});

        public static TextPosition[] Positions =
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

        public static SelectedTextBounds[] Bounds =
        {
            new SelectedTextBounds(new TextPosition(0, 0), new TextPosition(0, 3)),
            new SelectedTextBounds(new TextPosition(0, 0), new TextPosition(0, 0)),
            new SelectedTextBounds(new TextPosition(0, 0), new TextPosition(1, 0)),
            new SelectedTextBounds(new TextPosition(0, 0), new TextPosition(9, 5)),
            new SelectedTextBounds(new TextPosition(1, 0), new TextPosition(2, 7)),
            new SelectedTextBounds(new TextPosition(2, 3), new TextPosition(6, 3)),
            new SelectedTextBounds(new TextPosition(3, 1), new TextPosition(7, 7)),
            new SelectedTextBounds(new TextPosition(0, 3), new TextPosition(0, 0)),
            new SelectedTextBounds(new TextPosition(1, 0), new TextPosition(0, 0)),
            new SelectedTextBounds(new TextPosition(9, 5), new TextPosition(0, 0)),
            new SelectedTextBounds(new TextPosition(2, 7), new TextPosition(1, 0)),
            new SelectedTextBounds(new TextPosition(6, 3), new TextPosition(2, 3)),
            new SelectedTextBounds(new TextPosition(7, 7), new TextPosition(3, 1)),
        };
    }
}