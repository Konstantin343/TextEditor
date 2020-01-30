using System.Windows.Media;
using TextEditComponent.TextEditComponent;

namespace TextEditor.Themes
{
    internal class Theme
    {
        public Brush Background { get; set; }
        public Brush TextBrush { get; set; }
        public string Name { get; }

        public Theme(string name)
        {
            Name = name;
        }

        public void SetToTextEditBox(TextEditBox textEditBox)
        {
            textEditBox.Background = Background;
            textEditBox.TextLines.TextBrush = TextBrush;
            textEditBox.InvalidateAll();
        }
    }
}