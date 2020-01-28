using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.TestData;

namespace TestTextEditor.Tests.DataProviders
{
    public class FileMenuProviders
    {
        public static IEnumerable SaveAsProviders
        {
            get
            {
                var filePath = EnvironmentHelper.GetOutputPath("test.txt");
                var testData = BaseTestObjects.BaseMultilineTexts.Select(t => t.Item1);

                yield return new TestCaseData(testData.First(), filePath).SetName("Test1");
            }
        }
    }
}