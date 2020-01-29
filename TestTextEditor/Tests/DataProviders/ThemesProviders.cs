using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestTextEditor.Tests.TestData;

namespace TestTextEditor.Tests.DataProviders
{
    public class ThemesProviders
    {
        public static IEnumerable SelectThemesProviders => BaseTestObjects.BaseThemes
            .Select(theme => new TestCaseData(theme).SetName(theme + "_{m}"));
    }
}