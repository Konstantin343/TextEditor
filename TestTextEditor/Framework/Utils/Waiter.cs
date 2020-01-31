using System;
using System.Threading;

namespace TestTextEditor.Framework.Utils
{
    public static class Waiter
    {
        public static void WaitUntil(Func<bool> func, int timeout = 1000, int interval = 100)
        {
            var currentTime = 0;
            while (!func() && currentTime < timeout)
            {
                Thread.Sleep(interval);
                currentTime += interval;
            }
        }
    }
}