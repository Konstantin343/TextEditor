using System.Collections.Generic;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.TestData
{
    public static class BaseTestsObjects
    {
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