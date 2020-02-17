namespace TextEditComponent.TextEditComponent.Text
{
    public class TextLineEventArgs
    {
        public int Index { get; set; }

        public int Count { get; set; }

        public TextLineEventArgs(int from, int to = 1)
        {
            Index = from;
            Count = to;
        }
    }
}