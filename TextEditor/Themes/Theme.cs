using System.Windows.Input;
using System.Windows.Media;
using TextEditor.ViewModel;

namespace TextEditor.Themes
{
    public class Theme : BaseNotifyPropertyChanged
    {
        private ThemesManager _owner;

        private Brush _background;

        private Brush _textBrush;

        private bool _isSelected;

        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public Brush TextBrush
        {
            get => _textBrush;
            set
            {
                _textBrush = value;
                OnPropertyChanged(nameof(TextBrush));
            }
        }

        public string Name { get; }

        public Theme(string name, ThemesManager owner = null)
        {
            Name = name;
            _owner = owner;
        }

        private ICommand _selectThemeCommand;

        public ICommand SelectThemeCommand =>
            _selectThemeCommand ??
            (_selectThemeCommand = new RelayCommand(obj => { _owner.SelectTheme(Name); }));

        public override string ToString() => Name;

        public Theme SetOwner(ThemesManager owner)
        {
            _owner = owner;
            return this;
        }
    }
}