using System.Collections.Generic;

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

        public static readonly (int, int, int, int)[] BoundsSelectedTests = {
            (0, 0, 7, 14),
            (0, 3, 0, 11),
            (1, 5, 6, 7),
            (3, 0, 3, 4),
            (5, 0, 6, 0),
            (0, 4, 7, 12),
            (6, 5, 7, 6)
        };
    }
}