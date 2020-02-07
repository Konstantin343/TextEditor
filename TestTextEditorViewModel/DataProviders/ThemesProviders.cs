using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace TestTextEditorViewModel.DataProviders
{
    public class ThemesProviders
    {
        private static IList<string> _allThemes = new List<string>(new[]
            {"Gold", "Classic", "Black", "White"});

        public static IEnumerable AllThemesProviders
        {
            get
            {
                yield return new TestCaseData(_allThemes)
                    .SetName("AllThemesTest");
            }
        }

        public static IEnumerable ThemesProvider =>
            _allThemes.Select(theme => new TestCaseData(theme)
                .SetName(theme + "_{m}"));
    }
}