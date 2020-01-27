using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestStack.White.WindowsAPI;

namespace TestTextEditor.Tests.DataProviders
{
    public class FocusPositionProviders
    {
        public static IEnumerable ArrowsProvider =>
            new[]
            {
                KeyboardInput.SpecialKeys.UP,
                KeyboardInput.SpecialKeys.DOWN,
                KeyboardInput.SpecialKeys.RIGHT,
                KeyboardInput.SpecialKeys.LEFT
            }.Select(arrow => new TestCaseData(arrow).SetName(arrow + "_{m}"));
    }
}