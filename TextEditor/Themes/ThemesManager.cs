using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TextEditor.ViewModel;

namespace TextEditor.Themes
{
    public class ThemesManager : BaseNotifyPropertyChanged
    {
        public ObservableCollection<Theme> Themes { get; set; }

        private Theme _currentTheme;

        public Theme CurrentTheme
        {
            get => _currentTheme;
            private set
            {
                _currentTheme = value;
                OnPropertyChanged(nameof(CurrentTheme));
            }
        }

        public ThemesManager(IEnumerable<Theme> themes)
        {
            Themes = new ObservableCollection<Theme>(themes.Select(t => t.SetOwner(this)));
            CurrentTheme = Themes.FirstOrDefault();
            if (CurrentTheme != null) CurrentTheme.IsSelected = true;
        }

        public void SelectTheme(string themeName)
        {
            var newTheme = Themes.FirstOrDefault(theme => theme.Name == themeName);
            if (newTheme == null) return;
            CurrentTheme.IsSelected = false;
            CurrentTheme = newTheme;
            CurrentTheme.IsSelected = true;
        }
    }
}