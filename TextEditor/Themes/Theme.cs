using System.Windows.Media;
using Utils;

namespace TextEditor.Themes
{
    public class Theme : BaseNotifyPropertyChanged
    {
        public int T => 12;
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

        public Theme(string name, ThemesService owner = null)
        {
            Name = name;
        }
        
        public override string ToString() => Name;
    }
}