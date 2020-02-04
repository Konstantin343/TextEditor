using System.Collections.ObjectModel;

namespace TextEditor.Themes
{
    public interface IThemesService
    {
        ObservableCollection<Theme> Themes { get; }
        
        Theme CurrentTheme { get; }

        void SelectTheme(string themeName);
    }
}