namespace TextEditComponent.TextEditComponent.Text
{
    public class TextLineEventArgs
    {
        public int Index { get; set; }

        public TextLineEventArgs(int on) => Index = @on;
    }
}