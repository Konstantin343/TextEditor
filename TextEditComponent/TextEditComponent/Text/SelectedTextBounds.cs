namespace TextEditComponent.TextEditComponent.Text
{
    internal class SelectedTextBounds
    {
        internal TextPosition RealStart =>
            IsStartGreaterThanEnd ? MouseSelectionEnd : MouseSelectionStart;
        
        internal TextPosition RealEnd =>
            IsStartGreaterThanEnd ? MouseSelectionStart : MouseSelectionEnd;

        internal SelectedTextBounds() => Invalidate();

        internal TextPosition MouseSelectionStart { get; set; }
        internal TextPosition MouseSelectionEnd { get; set; }

        internal void SetBounds(TextPosition textPosition)
        {
            MouseSelectionStart = new TextPosition(textPosition);
            MouseSelectionEnd = new TextPosition(textPosition);
        }
        
        internal bool IsEmpty => RealStart.Equals(RealEnd);

        internal bool IsOnOneLine => RealStart.Str == RealEnd.Str;

        internal void Invalidate()
        {
            MouseSelectionStart = new TextPosition();
            MouseSelectionEnd = new TextPosition();
        }
        
        private bool IsStartGreaterThanEnd =>
            MouseSelectionStart.Str > MouseSelectionEnd.Str
            || MouseSelectionStart.Str == MouseSelectionEnd.Str
            && MouseSelectionStart.Chr > MouseSelectionEnd.Chr;
    }
}