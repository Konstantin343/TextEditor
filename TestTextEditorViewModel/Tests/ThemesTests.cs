using System.Collections.Generic;
using NUnit.Framework;
using TestTextEditorViewModel.DataProviders;

namespace TestTextEditorViewModel.Tests
{
    [TestFixture]
    public class ThemesTest : BaseTests
    {
        [Test]
        public void Test1()
        {
            //
            TestViewModel.NewFile();
            TestViewModel.OpenFile("");
            TestViewModel.SaveFile();
            TestViewModel.SaveAsFile("");
            //
        }

        [Test]
        [TestCaseSource(typeof(ThemesProviders), nameof(ThemesProviders.AllThemesProviders))]
        public void AllThemesTest(
            IList<string> themes) =>
            CollectionAssert.AreEquivalent(themes, TestViewModel.Themes,
                "Not all themes exist");

        [Test]
        [TestCaseSource(typeof(ThemesProviders), nameof(ThemesProviders.ThemesProvider))]
        public void ThemeTest(
            string theme)
        {
            TestViewModel.SelectTheme(theme);
            Assert.AreEqual(theme, TestViewModel.CurrentTheme);
        }
    }
}