using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TextEditor.TextEditComponent;

namespace TextEditor.Themes
{
    internal class ThemesManager
    {
        public TextEditBox TextEditBox { get; }
        public IList<Theme> Themes { get; }
        public Theme CurrentTheme { get; private set; }

        public ThemesManager(TextEditBox textEditBox)
        {
            TextEditBox = textEditBox;
            Themes = BasicThemes.AllBasicThemes;
            CurrentTheme = Themes.FirstOrDefault();
            CurrentTheme?.SetToTextEditBox(textEditBox);
        }

        public IEnumerable<MenuItem> GetThemesAsMenuItems(RoutedEventHandler themeOnClick) =>
            Themes.Select(theme =>
                {
                    var item = new MenuItem
                    {
                        Header = (CurrentTheme == theme ? "• " : "") + theme.Name,
                        Uid = theme.Name + "Theme"
                    };
                    item.Click += themeOnClick;
                    return item;
                })
                .ToList();

        public void SelectTheme(string themeName)
        {
            var newTheme = Themes.FirstOrDefault(theme => theme.Name == themeName);
            if (newTheme == null)
            {
                return;
            }

            newTheme.SetToTextEditBox(TextEditBox);
            CurrentTheme = newTheme;
        }
    }
}