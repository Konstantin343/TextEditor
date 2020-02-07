using NUnit.Framework;
using TestTextEditor.Tests.DataProviders;
using TestTextEditor.Tests.TestData;

namespace TestTextEditor.Tests
{
    [TestFixture]
    public class ThemesMenuTests : BaseTests
    {
        [Test]
        [TestCaseSource(typeof(ThemesProviders), nameof(ThemesProviders.SelectThemesProviders))]
        public void SelectThemeTest(
            string theme)
        {
            var themesMenuForm = MainWindow.ThemesMenuForm;
            themesMenuForm.Click();
            themesMenuForm.SelectTheme(theme);
            
            themesMenuForm.Click();
            Assert.AreEqual(theme, themesMenuForm.SelectedTheme);
        }

        [Test]
        public void AllThemesTest()
        {
            var themesMenuForm = MainWindow.ThemesMenuForm;
            themesMenuForm.Click();
            CollectionAssert.AreEquivalent(
                BaseTestObjects.BaseThemes, 
                themesMenuForm.AllThemes);
        }
    }
}