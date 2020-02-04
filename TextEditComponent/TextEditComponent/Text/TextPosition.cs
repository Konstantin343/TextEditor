namespace TextEditComponent.TextEditComponent.Text
{
    public class TextPosition
    {
        public TextPosition()
        {
            Str = 0;
            Chr = 0;
        }

        public TextPosition(TextPosition toCopy)
        {
            Str = toCopy.Str;
            Chr = toCopy.Chr;
        }

        public TextPosition(int str, int chr)
        {
            Str = str;
            Chr = chr;
        }
        
        public int Str { get; set; }
        public int Chr { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj is TextPosition textBound)
                return Str == textBound.Str && Chr == textBound.Chr;
            return false;
        }
    }
}