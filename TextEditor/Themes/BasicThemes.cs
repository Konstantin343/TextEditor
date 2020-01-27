using System.Collections.Generic;
using System.Windows.Media;

namespace TextEditor.Themes
{
    internal static class BasicThemes
    {
        public static readonly Theme Classic = new Theme("Classic")
        {
            Background = Brushes.DarkSlateGray,
            TextBrush = Brushes.White
        };

        public static readonly Theme Gold = new Theme("Gold")
        {
            Background = Brushes.RosyBrown,
            TextBrush = Brushes.Gold
        };

        public static readonly Theme Black = new Theme("Black")
        {
            Background = Brushes.Black,
            TextBrush = Brushes.White
        };

        public static readonly Theme White = new Theme("White")
        {
            Background = Brushes.White,
            TextBrush = Brushes.Black
        };

        public static readonly IList<Theme> AllBasicThemes = new List<Theme>(new[]
        {
            Classic, Gold, Black, White
        });
    }
}