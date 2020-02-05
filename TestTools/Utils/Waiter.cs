using System;
using System.Threading;

namespace TestTools.Utils
{
    public static class Waiter
    {
        public static void WaitUntil(Func<bool> func, int timeout = 2500, int interval = 100)
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