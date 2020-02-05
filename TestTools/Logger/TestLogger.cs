using System;

namespace TestTools.Logger
{
    public class TestLogger
    {
        private static TestLogger _instance;

        public static TestLogger Instance =>
            _instance ?? (_instance = new TestLogger());

        public void Info(object info) => Print(TestLoggerTypes.Info, info);

        public void Error(object error) => Print(TestLoggerTypes.Error, error);

        public void Warn(object warning) => Print(TestLoggerTypes.Warning, warning);

        public void Debug(object debugInfo) => Print(TestLoggerTypes.Debug, debugInfo);

        private void Print(TestLoggerTypes type, object toPrint) =>
            Console.WriteLine($@"[{DateTime.UtcNow}] {type} :: {toPrint}");
    }
}