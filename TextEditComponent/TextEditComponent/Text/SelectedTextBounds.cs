namespace TextEditComponent.TextEditComponent.Text
{
    public class SelectedTextBounds
    {
        public TextPosition RealStart =>
            IsStartGreaterThanEnd ? MouseSelectionEnd : MouseSelectionStart;
        
        public TextPosition RealEnd =>
            IsStartGreaterThanEnd ? MouseSelectionStart : MouseSelectionEnd;

        public SelectedTextBounds() => Invalidate();

        public TextPosition MouseSelectionStart { get; set; }
        public TextPosition MouseSelectionEnd { get; set; }

        public void SetBounds(TextPosition textPosition)
        {
            MouseSelectionStart = new TextPosition(textPosition);
            MouseSelectionEnd = new TextPosition(textPosition);
        }
        
        public bool IsEmpty => RealStart.Equals(RealEnd);

        public bool IsOnOneLine => RealStart.Str == RealEnd.Str;

        public void Invalidate()
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