using System.Windows.Input;
using System.Windows.Media;
using TextEditor.ViewModel;

namespace TextEditor.Themes
{
    public class Theme : BaseNotifyPropertyChanged
    {
        private Brush _background;

        private Brush _textBrush;

        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
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
        }
        
        public override string ToString() => Name;
    }
}