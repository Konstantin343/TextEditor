using System.Threading;
using System.Windows;

namespace TestTextEditor.Framework.Utils
{
    public static class ClipboardHelper
    {
        public static string GetText(string expected)
        {
            string result = null;
            Waiter.WaitUntil(() =>
            {
                var thread = new Thread(() => result = Clipboard.GetText());
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                return result == expected && result != null;
            });
            return result;
        }
        
        public static void SetText(string text)
        {
            var thread = new Thread(() => Clipboard.SetText(text));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
    }
}