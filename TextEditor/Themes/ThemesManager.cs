﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TextEditor.ViewModel;

namespace TextEditor.Themes
{
    public class ThemesManager : BaseNotifyPropertyChanged
    {
        public ObservableCollection<Theme> Themes { get; }

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
            Themes = new ObservableCollection<Theme>(themes);
            CurrentTheme = Themes.FirstOrDefault();
        }

        public void SelectTheme(string themeName)
        {
            var newTheme = Themes.FirstOrDefault(theme => theme.Name == themeName);
            if (newTheme == null) return;
            CurrentTheme = newTheme;
        }
    }
}