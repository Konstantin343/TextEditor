namespace TextEditComponent.TextEditComponent.Text
{
    internal class TextPosition
    {
        internal TextPosition()
        {
            Str = 0;
            Chr = 0;
        }

        internal TextPosition(TextPosition toCopy)
        {
            Str = toCopy.Str;
            Chr = toCopy.Chr;
        }

        internal TextPosition(int str, int chr)
        {
            Str = str;
            Chr = chr;
        }
        
        internal int Str { get; set; }
        internal int Chr { get; set; }

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