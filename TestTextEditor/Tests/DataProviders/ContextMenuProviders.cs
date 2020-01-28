using System.Collections;
using System.Linq;
using System.Windows;
using NUnit.Framework;

namespace TestTextEditor.Tests.DataProviders
{
    public class ContextMenuProviders
    {
        public static IEnumerable ContextMenuLocationProviders
        {
            get
            {
                return new[]
                {
                    new Point(0, 0),
                    new Point(5, 5),
                    new Point(40, 25),
                    new Point(30, 77),
                    new Point(100, 100),
                }.Select((point, i) => new TestCaseData(point).SetName($"TestCase{i + 1}" + "_{m}"));
            }
        }
    }
}