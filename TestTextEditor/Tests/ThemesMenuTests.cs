using NUnit.Framework;
using TestTextEditor.Tests.DataProviders;
using TestTextEditor.Tests.TestData;

namespace TestTextEditor.Tests
{
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
            Assert.IsTrue(themesMenuForm.IsOneSelectedTheme);
            CollectionAssert.AreEquivalent(BaseTestObjects.BaseThemes, themesMenuForm.AllThemes);
        }
    }
}