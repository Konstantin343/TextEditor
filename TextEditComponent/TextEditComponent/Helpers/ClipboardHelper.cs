using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace TextEditComponent.TextEditComponent.Helpers
{
    public static class ClipboardHelper
    {
        public static void SetText(string text, int retryCount = 10, int interval = 100)
        {
            DoWithRetries(() => Clipboard.SetDataObject(text, true), retryCount, interval);
        }

        public static string GetText(int retryCount = 10, int interval = 100)
        {
            var result = string.Empty;
            DoWithRetries(() => { result = Clipboard.GetText(); }, retryCount, interval);
            return result;
        }

        private static void DoWithRetries(Action action, int retryCount, int interval)
        {
            for (var i = 0; i < retryCount; i++)
            {
                try
                {
                    action();
                }
                catch (COMException)
                {
                    Thread.Sleep(interval);
                    continue;
                }

                break;
            }
        }
    }
}